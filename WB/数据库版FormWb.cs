using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Threading;

/*
 * 需要完善的：
 *     1.汉字转编码时，如果源是非汉字，则直接显示到目的框中。特别地，本想编码转汉字时，忘了选择，会出异常。
 *     2.编码转汉字时，同上。
 *     3.需要增加词组的编码转汉字功能。(词组的汉字转编码比较难，需要有中文识别技术，可不做)
 *     
 *     4.能否将数据压入程序中，启动后则直接在内存，用Linq查询，可提升效率。
 * 
 * 
 */

namespace WB
{
    public partial class FormWb : Form
    {
        //定义连接字串，声明连接
        private const string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=wb.mdb";
        private OleDbConnection connection;

        private delegate void DelegateNoParaMethod();       /*无参委托*/
        private delegate void DelegateMethod(string str);   /*带string参数委托*/

        private Thread thread;  //声明线程

        private string strSource;

        /// <summary>
        /// 窗体初始化
        /// </summary>
        public FormWb()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormWb_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);

            cmboxSelectDirect.SelectedIndex = 0;

            strSource = string.Empty;
        }
 

        /// <summary>
        /// 转换按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrans_Click(object sender, EventArgs e)
        {
            strSource = txtSource.Text.Trim();

            switch (cmboxSelectDirect.SelectedIndex)
            {
                case 0:
                    thread = new Thread(new ThreadStart(threadMethodWb2Code));
                    thread.Start();
                    break;
                case 1:
                    thread = new Thread(new ThreadStart(threadMethodCode2Wb));
                    thread.Start();
                    break;
            }
        }

        /// <summary>
        /// 线程方法－汉字转编码
        /// </summary>
        private void threadMethodWb2Code()
        {
            try
            {
                string sql = string.Empty;
                string[] strLine = Regex.Split(strSource, "\r\n");
                string strRetTemp;
                string strRet = string.Empty;

                char[] chrSource;
                string strCurrent;

                for (int i = 0; i < strLine.Length; i++)
                {
                    chrSource = strLine[i].ToCharArray();

                    foreach (char chr in chrSource)
                    {
                        strCurrent = chr.ToString();

                        if (strCurrent != " ")
                        {
                            lock (connection)
                            {
                                sql = "select wb from wb where character = '" + strCurrent + "' order by wb ";
                                OleDbCommand cmd = new OleDbCommand(sql, connection);
                                connection.Open();

                                if (cmd.ExecuteScalar() != null)
                                {
                                    strRetTemp = cmd.ExecuteScalar().ToString().Trim();

                                    if (strRetTemp.Length < 4)
                                    {
                                        strRetTemp += " ";
                                    }
                                }
                                else
                                {
                                    strRetTemp = strCurrent;
                                }

                                connection.Close();
                            }
                        }
                        else
                        {
                            strRetTemp = strCurrent;
                        }

                        strRet += strRetTemp;

                        
                    }

                    strRet += "\r\n";
                }

                this.BeginInvoke(new DelegateMethod(SetDes), new object[] { strRet });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                lock (connection)
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }

        }


        /// <summary>
        /// 线程方法－编码转汉字
        /// </summary>
        private void threadMethodCode2Wb()
        {
            try
            {
                string sql = string.Empty;
                string[] strLine = Regex.Split(strSource, "\r\n");
                string[] arrSource;

                string strRetTemp = string.Empty;
                string strRet = string.Empty;

                string strCurrent;

                for (int i = 0; i < strLine.Length; i++)
                {
                    arrSource = Regex.Split(strLine[i], " ");

                    for (int k = 0; k < arrSource.Length; k++)
                    {
                        strCurrent = arrSource[k];

                        if (strCurrent.Length < 4) //小于4的情况，即简码
                        {
                            lock (connection)
                            {
                                sql = "select top 1 [character] from [wb] where left(trim(wb)," + strCurrent.Length.ToString() + ") = '" + strCurrent + "'";
                                OleDbCommand cmd = new OleDbCommand(sql, connection);
                                connection.Open();

                                if (cmd.ExecuteScalar() != null)
                                {
                                    strRetTemp = cmd.ExecuteScalar().ToString().Trim();
                                }
                                else
                                {
                                    strRetTemp = strCurrent;
                                }

                                strRet += strRetTemp;

                                connection.Close();
                            }
                        }
                        else  //在split拆分空格后，字符长度仍然大于等于4的情况，需要做为词组来处理
                        {
                            int searchI = strCurrent.Length / 4;
                            int intMod = strCurrent.Length % 4;

                            for (int s = 0; s < searchI; s++)
                            {
                                string currentGroup = strCurrent.Substring(s * 4, 4);
                                
                                lock (connection)
                                {
                                    sql = "select top 1 [character] from [wb] where left(trim(wb)," + currentGroup.Length.ToString() + ") = '" + currentGroup + "'";
                                    OleDbCommand cmd = new OleDbCommand(sql, connection);
                                    connection.Open();

                                    if (cmd.ExecuteScalar() != null)
                                    {
                                        strRetTemp = cmd.ExecuteScalar().ToString().Trim();
                                    }
                                    else
                                    {
                                        strRetTemp = strCurrent;
                                    }

                                    strRet += strRetTemp;

                                    connection.Close();
                                }
                            }

                            if (intMod > 0)
                            {
                                string currentGroup = strCurrent.Substring(searchI * 4, strCurrent.Length - searchI * 4);
                                lock (connection)
                                {
                                    sql = "select top 1 [character] from [wb] where left(trim(wb)," + currentGroup.Length.ToString() + ") = '" + currentGroup + "'";
                                    OleDbCommand cmd = new OleDbCommand(sql, connection);
                                    connection.Open();

                                    if (cmd.ExecuteScalar() != null)
                                    {
                                        strRetTemp = cmd.ExecuteScalar().ToString().Trim();
                                    }
                                    else
                                    {
                                        strRetTemp = strCurrent;
                                    }

                                    strRet += strRetTemp;

                                    connection.Close();
                                }
                            }

                        }

                    }
                    strRet += "\r\n";

                }

                this.BeginInvoke(new DelegateMethod(SetDes), new object[] { strRet });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                lock (connection)
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 设置输出控件
        /// </summary>
        /// <param name="content"></param>
        private void SetDes(string content)
        {
            txtDes.Clear();
            txtDes.Text = content;
        }

 
    }
}

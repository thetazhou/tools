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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WB
{
    public partial class FormWb : Form
    {
        //定义连接字串，声明连接
        //private const string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=wb.mdb";
        //private OleDbConnection connection;

        private delegate void DelegateNoParaMethod();       /*无参委托*/
        private delegate void DelegateMethod(string str);   /*带string参数委托*/

        private Thread thread;  //声明线程

        private string strSource;

        private Hashtable htList1 = new Hashtable();
        private Hashtable htList2 = new Hashtable();

        private const int SetHashTable = 1;  //0为设置hashtable数据源,   1为发行版设置，表示正常读取

        HtList1 ht1;
        HtList2 ht2;

        private bool isInit = false;

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
            cmboxSelectDirect.SelectedIndex = 0;

            strSource = string.Empty;

            #region SetHashTable
            if (0 == SetHashTable)
            {
                //#region InitFile


                //connection = new OleDbConnection(connectionString);

                //string sql = "select character,wb from wb order by wb ";
                //OleDbCommand cmd = new OleDbCommand(sql, connection);
                //connection.Open();
                //OleDbDataReader dr = cmd.ExecuteReader();

                //while (dr.Read())
                //{
                //    if (false == htList1.Contains(dr[0].ToString().Trim()))
                //    {
                //        htList1.Add(dr[0].ToString().Trim(), dr[1].ToString().Trim());
                //    }
                //    if (false == htList2.Contains(dr[1].ToString().Trim()))
                //    {
                //        htList2.Add(dr[1].ToString().Trim(), dr[0].ToString().Trim());
                //    }
                //}

                //connection.Close();

                //StreamWriter sw = new StreamWriter("HtList2.cs", false, Encoding.Default);
                //string strContent = "\r\n";

                //string strMoban = File.ReadAllText("HashMoban.cs", Encoding.Default).Replace("HashClassName", "HtList2");
                //sw.Write(Regex.Split(strMoban,"//代码模板//")[0].ToString());

                //foreach (DictionaryEntry de in htList2)
                //{
                //    strContent = "Item.Add(\"" + de.Key.ToString() + "\",\"" + de.Value.ToString() + "\");\r\n";
                //    sw.Write(strContent);
                //}
                
                //sw.Write(Regex.Split(strMoban, "//代码模板//")[1].ToString());
                //sw.Close();


                //#endregion
            }
            else
            {
                #region ReadFile
                thread = new Thread(new ThreadStart(InitSource));
                thread.Start();
                #endregion
            }

            #endregion

        }

        private void InitSource()
        {
            try
            {
                 ht1 = new HtList1();
                htList1 = ht1.Item;

                 ht2 = new HtList2();
                htList2 = ht2.Item;

                this.BeginInvoke(new DelegateNoParaMethod(SetInit));

            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化出错，请联系开发人员！");
            }
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
                case 2:
                    thread = new Thread(new ThreadStart(threadMethodWord216));
                    thread.Start();
                    break;
                case 3:
                    thread = new Thread(new ThreadStart(threadMethod162Word));
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
                            lock (ht1)
                            {
                                Object objValue = htList1[strCurrent];

                                if (objValue != null)
                                {
                                    strRetTemp = (String)objValue;

                                    if (strRetTemp.Length < 4)
                                    {
                                        strRetTemp += " ";
                                    }
                                }
                                else
                                {
                                    strRetTemp = strCurrent;
                                }
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
                string strFilter;

                for (int i = 0; i < strLine.Length; i++)
                {
                    arrSource = strLine[i].Split(' ');

                    for (int k = 0; k < arrSource.Length; k++)
                    {
                        strCurrent = arrSource[k];

                        strFilter = "";

                        if (strCurrent.Length < 4) 
                        {
                            lock (ht1)
                            {
                                FilterWord(strCurrent,out strCurrent,out strFilter);

                                Object objValue = htList2[strCurrent];

                                if (objValue != null)
                                {
                                    strRetTemp = (String)objValue;
                                }
                                else
                                {
                                    strRetTemp = strCurrent;
                                }

                                strRet += strFilter + strRetTemp;
                            }
                        }
                        else  //在split拆分空格后，字符长度仍然大于等于4的情况，需要做为词组来处理
                        {
                            FilterWord(strCurrent, out strCurrent, out strFilter);
                            strRet += strFilter;

                            int searchI = strCurrent.Length / 4;
                            int intMod = strCurrent.Length % 4;

                            for (int s = 0; s < searchI; s++)
                            {
                                string currentGroup = strCurrent.Substring(s * 4, 4);

                                lock (ht1)
                                {
                                    FilterWord(currentGroup, out currentGroup, out strFilter);

                                    Object objValue = htList2[currentGroup];

                                    if (objValue != null)
                                    {
                                        strRetTemp = (String)objValue;
                                    }
                                    else
                                    {
                                        strRetTemp = currentGroup;
                                    }

                                    strRet += strFilter + strRetTemp;
                                }
                            }

                            if (intMod > 0)
                            {
                                string currentGroup = strCurrent.Substring(searchI * 4, strCurrent.Length - searchI * 4);
                               
                                lock (ht1)
                                {
                                    FilterWord(currentGroup, out currentGroup, out strFilter);

                                    Object objValue = htList2[currentGroup];

                                    if (objValue != null)
                                    {
                                        strRetTemp = (String)objValue;
                                    }
                                    else
                                    {
                                        strRetTemp = currentGroup;
                                    }

                                    strRet += strRetTemp;
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
        }

        private string FilterWordBlank(string strWrod)
        {
            string strCurrent = string.Empty;

            strCurrent = Regex.Replace(strWrod, "[^a-yA-Y]+", " ");

            return strCurrent;
        }

        private void FilterWord(string strWrod,out string rightWord,out string fiterWord)
        {
            rightWord = strWrod;
            fiterWord = "";

            string strCurrent = strWrod;

            if (false == Regex.IsMatch(strCurrent, "^[a-yA-Y]+$"))
            {
                string[] strLast = Regex.Split(strCurrent, "[^a-yA-Y]+");
                for (int z = 0; z < strLast.Length; z++)
                {
                    if (strLast[z].Trim() != "")
                    {
                        rightWord = strLast[z];
                    }
                    else
                    {
                        fiterWord = Regex.Replace(strCurrent, "[a-yA-Y]+", "");
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


        private void SetInit()
        {
            isInit = true;
            lblState.Text = "Beta 1.0";

            btnTransState();
        }


        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            btnTransState();
        }

        private void btnTransState()
        {
            if ("" != txtSource.Text.Trim() && true == isInit)
            {
                btnTrans.Enabled = true;
            }
            else
            {
                btnTrans.Enabled = false;
            }
        }





        #region 16 and word

        /// <summary>
        /// 线程方法－16to word
        /// </summary>
        private void threadMethod162Word()
        {
            try
            {
                string strRet = UnicodeToGB(strSource.Replace("\\u", ""));
                this.BeginInvoke(new DelegateMethod(SetDes), new object[] { strRet });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 线程方法－word to 16
        /// </summary>
        private void threadMethodWord216()
        {
            try
            {
                string strRet = ConvertToUTF8(strSource);
                this.BeginInvoke(new DelegateMethod(SetDes), new object[] { strRet });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// 由16进制转为汉字字符串（如：B2E2 -> 测 ）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>

        private string UnicodeToGB(string strDecode)
        {
            string sResult = "";
            for (int i = 0; i < strDecode.Length / 4; i++)
            {
                sResult += (char)short.Parse(strDecode.Substring(i * 4, 4), global::System.Globalization.NumberStyles.HexNumber);
            }
            return sResult;
        }



        /// <summary>
        /// 字符串转为UTF8字符串（如：测试 -> \u6d4b\u8bd5）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>

        public string ConvertToUTF8(string source)
        {
            StringBuilder sb = new StringBuilder();//UTF8
            string s1;
            string s2;
            for (int i = 0; i < source.Length; i++)
            {
                byte[] bt = System.Text.Encoding.Unicode.GetBytes(source.Substring(i, 1));
                if (bt.Length > 1)//判断是否汉字
                {
                    s1 = Convert.ToString((short)(bt[1] - '\0'), 16);//转化为16进制字符串
                    s2 = Convert.ToString((short)(bt[0] - '\0'), 16);//转化为16进制字符串
                    s1 = (s1.Length == 1 ? "0" : "") + s1;//不足位补0
                    s2 = (s2.Length == 1 ? "0" : "") + s2;//不足位补0
                    sb.Append("\\u" + s1 + s2);
                }
            }

            return sb.ToString();
        }





        #endregion

    }
}

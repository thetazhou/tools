using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WB
{
    partial class FormWb
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_CLOSE = 0x10;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWb));
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDes = new System.Windows.Forms.TextBox();
            this.cmboxSelectDirect = new System.Windows.Forms.ComboBox();
            this.btnTrans = new System.Windows.Forms.Button();
            this.picBoxClose = new System.Windows.Forms.PictureBox();
            this.pnlBorder = new System.Windows.Forms.Panel();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.picBoxSource = new System.Windows.Forms.PictureBox();
            this.picBoxMin = new System.Windows.Forms.PictureBox();
            this.picBoxTitle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxClose)).BeginInit();
            this.pnlBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(31, 84);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSource.Size = new System.Drawing.Size(409, 114);
            this.txtSource.TabIndex = 0;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            // 
            // txtDes
            // 
            this.txtDes.Location = new System.Drawing.Point(31, 233);
            this.txtDes.Multiline = true;
            this.txtDes.Name = "txtDes";
            this.txtDes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDes.Size = new System.Drawing.Size(409, 121);
            this.txtDes.TabIndex = 1;
            // 
            // cmboxSelectDirect
            // 
            this.cmboxSelectDirect.AllowDrop = true;
            this.cmboxSelectDirect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxSelectDirect.FormattingEnabled = true;
            this.cmboxSelectDirect.Items.AddRange(new object[] {
            "汉字 -> 五笔编码",
            "五笔编码 -> 汉字",
            "汉字 -> 十六进制",
            "十六进制 -> 汉字"});
            this.cmboxSelectDirect.Location = new System.Drawing.Point(31, 204);
            this.cmboxSelectDirect.Name = "cmboxSelectDirect";
            this.cmboxSelectDirect.Size = new System.Drawing.Size(147, 20);
            this.cmboxSelectDirect.TabIndex = 3;
            // 
            // btnTrans
            // 
            this.btnTrans.BackColor = System.Drawing.Color.Transparent;
            this.btnTrans.Enabled = false;
            this.btnTrans.Location = new System.Drawing.Point(198, 204);
            this.btnTrans.Name = "btnTrans";
            this.btnTrans.Size = new System.Drawing.Size(75, 23);
            this.btnTrans.TabIndex = 4;
            this.btnTrans.Text = "转换";
            this.btnTrans.UseVisualStyleBackColor = false;
            this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
            // 
            // picBoxClose
            // 
            this.picBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.picBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("picBoxClose.Image")));
            this.picBoxClose.Location = new System.Drawing.Point(446, 7);
            this.picBoxClose.Name = "picBoxClose";
            this.picBoxClose.Size = new System.Drawing.Size(17, 17);
            this.picBoxClose.TabIndex = 7;
            this.picBoxClose.TabStop = false;
            this.picBoxClose.MouseLeave += new System.EventHandler(this.picBoxClose_MouseLeave);
            this.picBoxClose.Click += new System.EventHandler(this.picBoxClose_Click);
            this.picBoxClose.MouseHover += new System.EventHandler(this.picBoxClose_MouseHover);
            // 
            // pnlBorder
            // 
            this.pnlBorder.BackColor = System.Drawing.Color.Transparent;
            this.pnlBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBorder.Controls.Add(this.lblState);
            this.pnlBorder.Controls.Add(this.lblCopyright);
            this.pnlBorder.Controls.Add(this.picBoxSource);
            this.pnlBorder.Controls.Add(this.picBoxClose);
            this.pnlBorder.Controls.Add(this.picBoxMin);
            this.pnlBorder.Controls.Add(this.picBoxTitle);
            this.pnlBorder.Controls.Add(this.btnTrans);
            this.pnlBorder.Controls.Add(this.txtDes);
            this.pnlBorder.Controls.Add(this.cmboxSelectDirect);
            this.pnlBorder.Controls.Add(this.txtSource);
            this.pnlBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlBorder.Name = "pnlBorder";
            this.pnlBorder.Size = new System.Drawing.Size(472, 398);
            this.pnlBorder.TabIndex = 8;
            this.pnlBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlBorder_MouseDown);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(29, 366);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(59, 12);
            this.lblState.TabIndex = 13;
            this.lblState.Text = "初始化...";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(267, 366);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(173, 12);
            this.lblCopyright.TabIndex = 12;
            this.lblCopyright.Text = "Copyright © 2000-2008,ZTOTEM";
            // 
            // picBoxSource
            // 
            this.picBoxSource.BackColor = System.Drawing.Color.Transparent;
            this.picBoxSource.Image = ((System.Drawing.Image)(resources.GetObject("picBoxSource.Image")));
            this.picBoxSource.Location = new System.Drawing.Point(31, 53);
            this.picBoxSource.Name = "picBoxSource";
            this.picBoxSource.Size = new System.Drawing.Size(55, 31);
            this.picBoxSource.TabIndex = 11;
            this.picBoxSource.TabStop = false;
            // 
            // picBoxMin
            // 
            this.picBoxMin.BackColor = System.Drawing.Color.Transparent;
            this.picBoxMin.Image = ((System.Drawing.Image)(resources.GetObject("picBoxMin.Image")));
            this.picBoxMin.Location = new System.Drawing.Point(423, 8);
            this.picBoxMin.Name = "picBoxMin";
            this.picBoxMin.Size = new System.Drawing.Size(17, 15);
            this.picBoxMin.TabIndex = 10;
            this.picBoxMin.TabStop = false;
            this.picBoxMin.MouseLeave += new System.EventHandler(this.picBoxMin_MouseLeave);
            this.picBoxMin.Click += new System.EventHandler(this.picBoxMin_Click);
            this.picBoxMin.MouseEnter += new System.EventHandler(this.picBoxMin_MouseHover);
            // 
            // picBoxTitle
            // 
            this.picBoxTitle.BackColor = System.Drawing.Color.Transparent;
            this.picBoxTitle.Image = ((System.Drawing.Image)(resources.GetObject("picBoxTitle.Image")));
            this.picBoxTitle.Location = new System.Drawing.Point(-1, -1);
            this.picBoxTitle.Name = "picBoxTitle";
            this.picBoxTitle.Size = new System.Drawing.Size(472, 50);
            this.picBoxTitle.TabIndex = 9;
            this.picBoxTitle.TabStop = false;
            this.picBoxTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxTitle_MouseDown);
            // 
            // FormWb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(472, 398);
            this.ControlBox = false;
            this.Controls.Add(this.pnlBorder);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormWb";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "五笔编码转换";
            this.Load += new System.EventHandler(this.FormWb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxClose)).EndInit();
            this.pnlBorder.ResumeLayout(false);
            this.pnlBorder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDes;
        private System.Windows.Forms.ComboBox cmboxSelectDirect;
        private System.Windows.Forms.Button btnTrans;
        private System.Windows.Forms.PictureBox picBoxClose;
        private Panel pnlBorder;
        private PictureBox picBoxMin;
        private PictureBox picBoxTitle;
        private PictureBox picBoxSource;



        /// <summary>
        /// pnlBorder拖动效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBorder_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// picBoxTitle拖动效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// 窗体最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 鼠标移至picBoxMin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxMin_MouseHover(object sender, EventArgs e)
        {
            this.picBoxMin.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// 鼠标离开picBoxMin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxMin_MouseLeave(object sender, EventArgs e)
        {
            this.picBoxMin.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// 鼠标移至picBoxClose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxClose_MouseHover(object sender, EventArgs e)
        {
            this.picBoxClose.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// 鼠标离开picBoxClose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxClose_MouseLeave(object sender, EventArgs e)
        {
            this.picBoxClose.BorderStyle = BorderStyle.None;
        }

        private Label lblCopyright;
        private Label lblState;
    }
}


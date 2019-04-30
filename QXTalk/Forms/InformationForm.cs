using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using CCWin.Win32;
using CCWin.Win32.Const;

namespace QXTalk.Forms
{
    /// <summary>
    /// 启动时，右下托盘提示窗口。
    /// </summary>
    public partial class InformationForm : BaseForm
    {
        public InformationForm(string id,string name, Image title)
        {
            InitializeComponent();           

            this.Text = string.Format("您好，{0}（{1}）",name, id);
            this.pnlImgTx.BackgroundImage = title;
            this.skinLabel2.Text = string.Format("欢迎使用 QXTalk \n\n毕业设计项目" ,GlobalResourceManager.SoftwareName);
        }

        //窗口加载时
        private void FrmInformation_Load(object sender, EventArgs e)
        {
            //初始化窗口出现位置
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.PointToScreen(p);
            this.Location = p;
            NativeMethods.AnimateWindow(this.Handle, 130, AW.AW_SLIDE + AW.AW_VER_NEGATIVE);//开始窗体动画
        }

        //倒计时6秒关闭弹出窗
        private void timShow_Tick(object sender, EventArgs e)
        {
            //鼠标不在窗体内时
            if (!this.Bounds.Contains(Cursor.Position))
            {
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("灵感来源于生活！");
        }

        private void skinButtom1_Click(object sender, EventArgs e)
        {

        }        
    }
}

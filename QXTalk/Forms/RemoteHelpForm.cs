using System;
using System.Windows.Forms;
using CCWin;
using ESBasic;
using OMCS.Passive;

namespace QXTalk.Forms
{
    /// <summary>
    /// 远程桌面/远程协助 窗口。
    /// </summary>
    public partial class RemoteHelpForm : BaseForm
    {
        private RemoteHelpStyle remoteDesktopStyle = RemoteHelpStyle.PartScreen;
        public event CbGeneric<bool ,RemoteHelpStyle> RemoteHelpEnded; //参数：true - 协助方终止；false - 请求方终止

        private string ownerName = "";
        public RemoteHelpForm(string ownerID, string nickName ,RemoteHelpStyle style)
        {
            InitializeComponent();
            this.CanResize = true;
            this.RemoteHelpEnded += delegate { };

            this.ownerName = nickName;
            this.Text = string.Format("远程协助{0} - {1}", this.remoteDesktopStyle == RemoteHelpStyle.PartScreen ? "（指定屏幕区域）" : "", this.ownerName);         
            this.Cursor = Cursors.WaitCursor;

            this.desktopConnector1.Visible = false;
            this.desktopConnector1.WatchingOnly = false; //可以操控桌面
            this.desktopConnector1.ConnectEnded += new CbGeneric<ConnectResult>(desktopConnector1_ConnectEnded);
            this.desktopConnector1.Disconnected += new CbGeneric<ConnectorDisconnectedType>(desktopConnector1_Disconnected);

            this.desktopConnector1.BeginConnect(ownerID);
        } 

        void desktopConnector1_Disconnected(ConnectorDisconnectedType disconnectedType)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<ConnectorDisconnectedType>(this.desktopConnector1_Disconnected), disconnectedType);
            }
            else
            {
                if (disconnectedType == ConnectorDisconnectedType.GuestActiveDisconnect)
                {
                    return;
                }

                MessageBoxEx.Show(string.Format("到{0}的桌面连接断开。原因：{1}", this.ownerName, disconnectedType));
                this.Close();
            }
        }

        void desktopConnector1_ConnectEnded(ConnectResult res)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<ConnectResult>(this.desktopConnector1_ConnectEnded), res);
            }
            else
            {
                this.Cursor = Cursors.Default;
                if (res == ConnectResult.Succeed)
                {
                    this.desktopConnector1.Visible = true;
                    this.skinComboBox_quality.Visible = true;
                    this.skinLabel_quality.Visible = true;

                    int quality = this.desktopConnector1.GetVideoQuality();
                    int index = (quality - 1) / 5;
                    if (index < 0)
                    {
                        index = 0;
                    }
                    if (index > 3)
                    {
                        index = 3;
                    }
                    this.skinComboBox_quality.SelectedIndex = index;

                    return;
                }

                MessageBoxEx.Show(string.Format("连接{0}的桌面失败。原因：{1}", this.ownerName, res));
                this.Close();
            }
        }

        private bool ownerCancel = false;
        internal void OwnerTeminateHelp()
        {
            this.ownerCancel = true;
            this.Close();
        }

        private void RemoteHelpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.desktopConnector1.Connected)
            {
                return;
            }

            this.desktopConnector1.Disconnect();
            this.RemoteHelpEnded(this.ownerCancel ,this.remoteDesktopStyle);
        }

        private void skinComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int quality = this.skinComboBox_quality.SelectedIndex * 5 + 1;
            this.desktopConnector1.ChangeOwnerDesktopEncodeQuality(quality);
        }       
    }

    public enum RemoteHelpStyle
    {
        PartScreen = 0 ,
        AllScreen
    }
}

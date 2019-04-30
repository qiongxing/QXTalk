using ESPlus.Application.FileTransfering.Passive;
using JustLib.NetworkDisk.Passive;

namespace QXTalk.Forms
{
    /// <summary>
    /// 网盘/远程磁盘 窗口。
    /// </summary>
    public partial class NDiskForm : BaseForm
    {        
        public NDiskForm(string ownerID,string nickName, IFileOutter fileOutter , INDiskOutter diskOutter)
        {
            InitializeComponent();
            this.TopMost = false;
            this.ShowInTaskbar = true;
            this.nDiskBrowser1.Initialize(ownerID, fileOutter, diskOutter);
            this.Text = ownerID == null ? "我的网盘" : string.Format("远程磁盘 - {0}", nickName);
        }

       
    }
}

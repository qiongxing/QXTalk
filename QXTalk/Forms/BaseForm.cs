using System;
using CCWin;

namespace QXTalk.Forms
{
    /// <summary>
    /// GGTalk中所有Form的基类。使其拥有一致的皮肤和Icon。
    /// </summary>
    public partial class BaseForm : CCSkinMain
    {
        public BaseForm()
        {
            InitializeComponent();            
        }

        #region UseCustomIcon
        private bool useCustomIcon = false;
        /// <summary>
        /// 是否使用自己的图标。
        /// </summary>
        public bool UseCustomIcon
        {
            get { return useCustomIcon; }
            set { useCustomIcon = value; }
        } 
        #endregion

        #region UseCustomBackImage
        private bool useCustomBackImage = false;
        /// <summary>
        /// 是否使用自己的背景图片
        /// </summary>
        public bool UseCustomBackImage
        {
            get { return useCustomBackImage; }
            set { useCustomBackImage = value; }
        } 
        #endregion

        private void OrayForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (!this.useCustomIcon)
                {
                    this.Icon = GlobalResourceManager.Icon64;
                }

                if (!this.useCustomBackImage)
                {
                    this.Back = GlobalResourceManager.MainBackImage;
                }                
            }           
        }

       

        
         
    }
}

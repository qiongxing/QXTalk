﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using QXTalk.Core;

namespace QXTalk
{
    public interface IHeadImageGetter
    {
        Image GetHeadImage(GGUser user);
    }

    public interface IUserInformationForm
    {
        void SetUser(GGUser user);
    }

    /// <summary>
    /// 帮助类。
    /// </summary>
    public static class JustHelper
    {
        #region FlashChatWindow
        [DllImport("User32")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);        

        /// <summary>
        /// 闪动窗口
        /// </summary>
        /// <param name="form">要闪动的窗口</param>
        public static void FlashChatWindow(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized || !form.Focused)
            {
                FlashWindow(form.Handle, true);
            }      
        }
        #endregion    
    }
}

using System.Runtime.InteropServices;

namespace QXTalk.Controls.ChatBox.Internals
{    
    [StructLayout(LayoutKind.Sequential)]
    public struct CHARRANGE
    {
        public int cpMin;
        public int cpMax;
    }
}

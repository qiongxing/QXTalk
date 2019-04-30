using System;
using System.Runtime.InteropServices;

namespace QXTalk.Controls.ChatBox.Internals
{
    [Flags, ComVisible(false)]
    public enum DVASPECT : int
    {
        DVASPECT_CONTENT = 1,
        DVASPECT_THUMBNAIL = 2,
        DVASPECT_ICON = 4,
        DVASPECT_DOCPRINT = 8,
        DVASPECT_OPAQUE = 16,
        DVASPECT_TRANSPARENT = 32,
    }
}

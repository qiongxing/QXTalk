﻿using System.Runtime.InteropServices;

namespace QXTalk.Controls.ChatBox.Internals
{
    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    public sealed class tagLOGPALETTE
    {
        [MarshalAs(UnmanagedType.U2)/*leftover(offset=0, palVersion)*/]
        public short palVersion;

        [MarshalAs(UnmanagedType.U2)/*leftover(offset=2, palNumEntries)*/]
        public short palNumEntries;

        // UNMAPPABLE: palPalEntry: Cannot be used as a structure field.
        //   /** @com.structmap(UNMAPPABLE palPalEntry) */
        //  public UNMAPPABLE palPalEntry;
    }
}

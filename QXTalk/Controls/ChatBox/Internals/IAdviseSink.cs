﻿using System.Runtime.InteropServices;

namespace QXTalk.Controls.ChatBox.Internals
{
    [ComVisible(true), 
    Guid("0000010F-0000-0000-C000-000000000046"), 
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAdviseSink
    {

        //C#r: UNDONE (Field in interface) public static readonly    Guid iid;
        void OnDataChange(
            [In]
			FORMATETC pFormatetc,
            [In]
			STGMEDIUM pStgmed);

        void OnViewChange(
            [In, MarshalAs(UnmanagedType.U4)]
			int dwAspect,
            [In, MarshalAs(UnmanagedType.I4)]
			int lindex);

        void OnRename(
            [In, MarshalAs(UnmanagedType.Interface)]
			object pmk);

        void OnSave();


        void OnClose();
    }
}

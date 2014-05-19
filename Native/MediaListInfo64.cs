using System;
using System.Runtime.InteropServices;

namespace Frost.SharpMediaInfo.Native {

    public static class MediaListInfo64 {

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Open")]
        public static extern IntPtr Open(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName, IntPtr options);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_New")]
        public static extern IntPtr New();

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Delete")]
        public static extern void Delete(IntPtr handle);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Close")]
        public static extern void Close(IntPtr handle, IntPtr filePos);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Inform")]
        public static extern IntPtr Inform(IntPtr handle, IntPtr filePos, IntPtr reserved);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_GetI")]
        public static extern IntPtr GetI(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Get")]
        public static extern IntPtr Get(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, IntPtr kindOfInfo, IntPtr kindOfSearch);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Option")]
        public static extern IntPtr Option(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_State_Get")]
        public static extern IntPtr StateGet(IntPtr handle);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Count_Get")]
        public static extern IntPtr CountGet(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber);
    }

}
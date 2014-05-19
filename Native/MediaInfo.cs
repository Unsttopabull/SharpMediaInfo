using System;
using System.Runtime.InteropServices;

namespace Frost.SharpMediaInfo.Native {
    public static class MediaInfo64 {

        //Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_New")]
        public static extern IntPtr New();

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Delete")]
        public static extern void Delete(IntPtr handle);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Open")]
        public static extern IntPtr Open(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfoA_Open")]
        public static extern IntPtr OpenAnsi(IntPtr handle, IntPtr fileName);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Close")]
        public static extern void Close(IntPtr handle);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Inform")]
        public static extern IntPtr Inform(IntPtr handle, IntPtr reserved);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfoA_Inform")]
        public static extern IntPtr InformAnsi(IntPtr handle, IntPtr reserved);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_GetI")]
        public static extern IntPtr GetI(IntPtr handle, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfoA_GetI")]
        public static extern IntPtr GetIAnsi(IntPtr handle, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Get")]
        public static extern IntPtr Get(IntPtr handle, IntPtr streamKind, IntPtr streamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, IntPtr kindOfInfo, IntPtr kindOfSearch);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfoA_Get")]
        public static extern IntPtr GetAnsi(IntPtr handle, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo, IntPtr kindOfSearch);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Option")]
        public static extern IntPtr Option(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfoA_Option")]
        public static extern IntPtr OptionAnsi(IntPtr handle, IntPtr option, IntPtr value);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_State_Get")]
        public static extern IntPtr StateGet(IntPtr handle);

        [DllImport("x86/MediaInfo.dll", EntryPoint = "MediaInfo_Count_Get")]
        public static extern IntPtr CountGet(IntPtr handle, IntPtr streamKind, IntPtr streamNumber);

    }
}

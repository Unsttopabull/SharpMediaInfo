using System;
using System.Runtime.InteropServices;

namespace Frost.SharpMediaInfo {

    /// <summary>Represents a MediaInfo file in a List.</summary>
    public class MediaListFile : MediaFileBase {
        /// <summary>Initializes a new instance of the <see cref="MediaListFile"/> class.</summary>
        /// <param name="mediaInfoList">The handle to the MediaInfoList.</param>
        /// <param name="filePos">The file position in list.</param>
        /// <param name="cacheInfom">If set to <c>true</c> the Iform() call will get cached to limit calls to DLL when cached.</param>
        /// <param name="allInfoCache">If set to <c>true</c> the Inform() call will get cached with ShowAllInfo set to <c>true</c>.</param>
        internal MediaListFile(IntPtr mediaInfoList, int filePos, bool cacheInfom, bool allInfoCache = true)
            : base(mediaInfoList) {
            DisposedMessage = FILE_CLOSED;

            IsOpen = true;
            FileIndex = filePos;
            InitializeMediaStreams(cacheInfom, allInfoCache);
        }

        /// <summary>Index of the file in the list.</summary>
        public int FileIndex { get; internal set; }

        #region P/Invoke C Functions x86

        [DllImport("x86/MediaInfo.dll")]
        private static extern void MediaInfoList_Close(IntPtr handle, IntPtr filePos);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Inform(IntPtr handle, IntPtr filePos, IntPtr reserved);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_GetI(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Get(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, IntPtr kindOfInfo, IntPtr kindOfSearch);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Option(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_State_Get(IntPtr handle);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Count_Get(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber);

        #endregion

        #region P/Invoke C Functions x64

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Close")]
        private static extern void MediaInfoList_Close_x64(IntPtr handle, IntPtr filePos);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Inform")]
        private static extern IntPtr MediaInfoList_Inform_x64(IntPtr handle, IntPtr filePos, IntPtr reserved);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_GetI")]
        private static extern IntPtr MediaInfoList_GetI_x64(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Get")]
        private static extern IntPtr MediaInfoList_Get_x64(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, IntPtr kindOfInfo, IntPtr kindOfSearch);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Option")]
        private static extern IntPtr MediaInfoList_Option_x64(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_State_Get")]
        private static extern IntPtr MediaInfoList_State_Get_x64(IntPtr handle);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Count_Get")]
        private static extern IntPtr MediaInfoList_Count_Get_x64(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber);

        #endregion

        #region IDisposable

        internal void Close() {
            Dispose(false);
        }

        /// <summary>Closes this instance and disposes all allocated resources.</summary>
        protected override void Dispose(bool destructor) {
            if (IsOpen) {
                if (Environment.Is64BitProcess) {
                    MediaInfoList_Close_x64(Handle, (IntPtr) FileIndex);
                }
                else {
                    MediaInfoList_Close(Handle, (IntPtr) FileIndex);
                }

                if (destructor) {
                    GC.SuppressFinalize(this);
                }

                IsOpen = false;
            }
        }

        #endregion

        #region MediaInfo List Interop

        /// <summary>Get all details about a file in one string</summary>
        /// <remarks>You can change default presentation with Inform_Set()</remarks>
        public override string Inform() {
            ThrowIfDisposed();

            return Marshal.PtrToStringUni(Environment.Is64BitProcess
                                              ? MediaInfoList_Inform_x64(Handle, (IntPtr) FileIndex, (IntPtr) 0)
                                              : MediaInfoList_Inform(Handle, (IntPtr) FileIndex, (IntPtr) 0));
        }

        /// <summary>Configure or get information about MediaInfoLib</summary>
        /// <param name="option">The name of option</param>
        /// <param name="value">The value of option</param>
        /// <returns>Depend of the option: by default "" (nothing) means No, other means Yes</returns>
        internal override string Option(String option, String value = "") {
            ThrowIfDisposed();

            return Marshal.PtrToStringUni(Environment.Is64BitProcess
                                              ? MediaInfoList_Option_x64(Handle, option, value)
                                              : MediaInfoList_Option(Handle, option, value));
        }

        /// <summary>Get the state of the library</summary>
        /// <remarks>NOT IMPLEMENTED YET</remarks>
        /// <returns> 
        /// <pre><b>below 1000</b> => No information is available for the file yet</pre>
        /// <pre><b>>= 1000 to 4999</b> => Only local (into the file) information is available, getting Internet information (titles only) is no finished yet</pre>
        /// <pre><b>5000</b> => (only if Internet connection is accepted) User interaction is needed (use Option() with "Internet_Title_Get") </pre>
        /// <pre><b>Warning:</b> even there is only one possible, user interaction (or the software) is needed</pre>
        /// <pre><b>5001 to 10000</b> =>	Only local (into the file) information is available, getting Internet information (all) is no finished yet</pre>
        /// <pre><b>below 10000</b> => Done</pre>
        /// </returns>
        public override int StateGet() {
            ThrowIfDisposed();

            return (int) (Environment.Is64BitProcess
                              ? MediaInfoList_State_Get_x64(Handle)
                              : MediaInfoList_State_Get(Handle));
        }

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in string format ("Codec", "Width"...) </param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <param name="kindOfSearch">Where to look for the parameter</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public override string Get(StreamKind streamKind, int streamNumber, string parameter,
                                   InfoKind kindOfInfo = InfoKind.Text, InfoKind kindOfSearch = InfoKind.Name) {
            ThrowIfDisposed();

            return Marshal.PtrToStringUni(Environment.Is64BitProcess
                                              ? MediaInfoList_Get_x64(Handle, (IntPtr) FileIndex, (IntPtr) streamKind, (IntPtr) streamNumber, parameter, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch)
                                              : MediaInfoList_Get(Handle, (IntPtr) FileIndex, (IntPtr) streamKind, (IntPtr) streamNumber, parameter, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch));
        }

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public override string Get(StreamKind streamKind, int streamNumber, int parameter, InfoKind kindOfInfo = InfoKind.Text) {
            ThrowIfDisposed();

            return Marshal.PtrToStringUni(Environment.Is64BitProcess
                ? MediaInfoList_GetI_x64(Handle, (IntPtr) FileIndex, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo)
                : MediaInfoList_GetI(Handle, (IntPtr) FileIndex, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo));
        }

        /// <summary>Count of streams of a stream kind or count of piece of information in this stream.</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in this kind of stream (first, second...)</param>
        /// <returns></returns>
        public override int CountGet(StreamKind streamKind, int streamNumber = -1) {
            ThrowIfDisposed();

            return (int) (Environment.Is64BitProcess
                ? MediaInfoList_Count_Get_x64(Handle, (IntPtr) FileIndex, (IntPtr) streamKind, (IntPtr) streamNumber)
                : MediaInfoList_Count_Get(Handle, (IntPtr) FileIndex, (IntPtr) streamKind, (IntPtr) streamNumber));
        }

        #endregion

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return General.FileInfo.FullPath;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <param name="full">If <c>true</c> returns full path; otherwise returns filename with extension.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(bool full) {
            return full ? General.FileInfo.FullPath : (General.FileInfo.FileName + "." + General.FileInfo.Extension);
        }
    }

}
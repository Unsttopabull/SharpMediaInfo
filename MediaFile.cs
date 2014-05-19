using System;
using System.Runtime.InteropServices;
using Frost.SharpMediaInfo.Native;

namespace Frost.SharpMediaInfo {

    /// <summary>Represent MediaInfo details about a single file.</summary>
    public class MediaFile : MediaFileBase {

        private static readonly bool MustUseAnsi;
        private bool _isDisposed;

        static MediaFile() {
            MustUseAnsi = Environment.OSVersion.ToString().IndexOf("Windows", StringComparison.Ordinal) == -1;
        }

        /// <summary>Initializes a new instance of the <see cref="MediaFile"/> class.</summary>
        /// <param name="filePath">The full path to the file from which to detect features.</param>
        /// <param name="cacheInfom">If set to <c>true</c> the Iform() call will get cached to limit calls to DLL when cached.</param>
        /// <param name="allInfoCache">If set to <c>true</c> the Inform() call will get cached with ShowAllInfo set to <c>true</c>.</param>
        public MediaFile(string filePath, bool cacheInfom, bool allInfoCache = true) : base(InitializeHandle()) {
            DisposedMessage = HANDLE_DISPOSED;

            IsOpen = Open(filePath);
            InitializeMediaStreams(cacheInfom, allInfoCache);
        }

        #region IDisposable

        /// <summary>Closes this instance and disposes all allocated resources.</summary>
        public void Close() {
            Dispose(false);
        }

        protected override void Dispose(bool destructor) {
            if (!_isDisposed) {
                if (IsOpen) {
                    CloseHandle(Handle);
                    IsOpen = false;
                }
                DeleteHandle(Handle);

                if (destructor) {
                    GC.SuppressFinalize(this);
                }
                _isDisposed = true;
            }
        }

        #endregion

        #region MediaInfo Interop

        private static IntPtr InitializeHandle() {
            IntPtr handle;
            if (Environment.Is64BitProcess) {
                try {
                    handle = MediaInfo64.New();
                }
                catch (BadImageFormatException) {
                    handle = MediaInfo.New();
                }
            }
            else {
                try {
                    handle = MediaInfo.New();
                }
                catch (BadImageFormatException) {
                    handle = MediaInfo64.New();
                }
            }
            return handle;
        }

        private void CloseHandle(IntPtr handle) {
            if (Environment.Is64BitProcess) {
                MediaInfo64.Close(handle);
            }
            else {
                MediaInfo.Close(handle);
            }
        }

        private void DeleteHandle(IntPtr handle) {
            if (Environment.Is64BitProcess) {
                MediaInfo64.Delete(handle);
            }
            else {
                MediaInfo.Delete(handle);
            }
        }

        /// <summary>Open a file and collect information about it (technical information and tags)</summary>
        /// <param name="fileName">Full name of the file to open.</param>
        /// <returns>Returns <c>true</c> if sucessfull, otherwise <c>false</c></returns>
        protected bool Open(string fileName) {
            if (MustUseAnsi) {
                IntPtr fileNamePtr = Marshal.StringToHGlobalAnsi(fileName);
                int toReturn = (int) (Environment.Is64BitProcess ? MediaInfo64.OpenAnsi(Handle, fileNamePtr) : MediaInfo.OpenAnsi(Handle, fileNamePtr));
                Marshal.FreeHGlobal(fileNamePtr);

                return toReturn == 1;
            }
            return (int) (Environment.Is64BitProcess ? MediaInfo64.Open(Handle, fileName) : MediaInfo.Open(Handle, fileName)) == 1;
        }

        /// <summary>Get all details about a file in one string</summary>
        /// <returns>string with all the file details</returns>
        /// <remarks>You can change default presentation with Inform_Set()</remarks>
        public override string Inform() {
            ThrowIfDisposed();

            if (MustUseAnsi) {
                return Marshal.PtrToStringAnsi(Environment.Is64BitProcess ? MediaInfo64.InformAnsi(Handle, (IntPtr) 0) : MediaInfo.InformAnsi(Handle, (IntPtr) 0));
            }
            return Marshal.PtrToStringUni(Environment.Is64BitProcess ? MediaInfo64.Inform(Handle, (IntPtr) 0) : MediaInfo.Inform(Handle, (IntPtr) 0));
        }

        /// <summary>Configure or get information about MediaInfoLib</summary>
        /// <param name="option">The option.</param>
        /// <param name="value">The value of option</param>
        /// <returns>Depend of the option: by default "" (nothing) means No, other means Yes</returns>
        internal override string Option(string option, string value = "") {
            ThrowIfDisposed();

            if (MustUseAnsi) {
                IntPtr optionPtr = Marshal.StringToHGlobalAnsi(option);
                IntPtr valuePtr = Marshal.StringToHGlobalAnsi(value);

                string toReturn = Marshal.PtrToStringAnsi(Environment.Is64BitProcess ? MediaInfo64.OptionAnsi(Handle, optionPtr, valuePtr) : MediaInfo.OptionAnsi(Handle, optionPtr, valuePtr));

                Marshal.FreeHGlobal(optionPtr);
                Marshal.FreeHGlobal(valuePtr);
                return toReturn;
            }

            return Marshal.PtrToStringUni(Environment.Is64BitProcess ? MediaInfo64.Option(Handle, option, value) : MediaInfo.Option(Handle, option, value));
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

            return (int) (Environment.Is64BitProcess ? MediaInfo64.StateGet(Handle) : MediaInfo.StateGet(Handle));
        }

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in string format ("Codec", "Width"...) </param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <param name="kindOfSearch">Where to look for the parameter</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public override string Get(StreamKind streamKind, int streamNumber, string parameter, InfoKind kindOfInfo = InfoKind.Text, InfoKind kindOfSearch = InfoKind.Name) {
            ThrowIfDisposed();

            if (MustUseAnsi) {
                IntPtr parameterPtr = Marshal.StringToHGlobalAnsi(parameter);
                string toReturn = Marshal.PtrToStringAnsi(Environment.Is64BitProcess 
                                                                ? MediaInfo64.GetAnsi(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, parameterPtr, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch)
                                                                : MediaInfo.GetAnsi(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, parameterPtr, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch));
                Marshal.FreeHGlobal(parameterPtr);
                return toReturn;
            }
            return Marshal.PtrToStringUni(Environment.Is64BitProcess 
                                                ? MediaInfo64.Get(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, parameter, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch)
                                                : MediaInfo.Get(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, parameter, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch));
        }

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public override string Get(StreamKind streamKind, int streamNumber, int parameter, InfoKind kindOfInfo = InfoKind.Text) {
            ThrowIfDisposed();

            return MustUseAnsi
                ? Marshal.PtrToStringAnsi(Environment.Is64BitProcess ? MediaInfo64.GetIAnsi(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo) : MediaInfo.GetIAnsi(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo))
                : Marshal.PtrToStringUni(Environment.Is64BitProcess ? MediaInfo64.GetI(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo) : MediaInfo.GetI(Handle, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo));
        }

        /// <summary>Count of streams of a stream kind or count of piece of information in this stream.</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in this kind of stream (first, second...)</param>
        /// <returns></returns>
        public override int CountGet(StreamKind streamKind, int streamNumber = -1) {
            ThrowIfDisposed();

            return (int) (Environment.Is64BitProcess ? MediaInfo64.CountGet(Handle, (IntPtr) streamKind, (IntPtr) streamNumber) : MediaInfo.CountGet(Handle, (IntPtr) streamKind, (IntPtr) streamNumber));
        }

        #endregion
    }

}
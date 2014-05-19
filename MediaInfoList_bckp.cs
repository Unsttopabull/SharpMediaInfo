using System;
using System.Runtime.InteropServices;

namespace Frost.SharpMediaInfo {
    public class MediaInfoList {
        //Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)
        private readonly IntPtr _handle;

        public MediaInfoList() {
            _handle = MediaInfoList_New();
        }

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_New();

        [DllImport("MediaInfo.dll")]
        private static extern void MediaInfoList_Delete(IntPtr handle);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Open(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName, IntPtr options);

        [DllImport("MediaInfo.dll")]
        private static extern void MediaInfoList_Close(IntPtr handle, IntPtr filePos);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Inform(IntPtr handle, IntPtr filePos, IntPtr reserved);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_GetI(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, IntPtr parameter, IntPtr kindOfInfo);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Get(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, IntPtr kindOfInfo, IntPtr kindOfSearch);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Option(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_State_Get(IntPtr handle);

        [DllImport("MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Count_Get(IntPtr handle, IntPtr filePos, IntPtr streamKind, IntPtr streamNumber);

        //MediaInfo class

        ~MediaInfoList() {
            MediaInfoList_Delete(_handle);
        }

        //Default values, if you know how to set default values in C#, say me
        /// <summary>Open one or more files and collect information about them (technical information and tags)</summary>
        /// <param name="fileName">Full name of file(s) to open or Full name of folder(s) to open (if multiple names, names must be separated by "|")</param>
        /// <returns>Number	of files successfuly added</returns>
        public void Open(String fileName) {
            Open(fileName, 0);
        }

        /// <summary>Open one or more files and collect information about them (technical information and tags)</summary>
        /// <param name="fileName">Full name of file(s) to open or Full name of folder(s) to open (if multiple names, names must be separated by "|")</param>
        /// <param name="options">FileOption_Recursive = Recursive mode for folders FileOption_Close = Close all already opened files before</param>
        /// <returns>Number	of files successfuly added</returns>
        public int Open(String fileName, InfoFileOptions options) {
            return (int) MediaInfoList_Open(_handle, fileName, (IntPtr) options);
        }

        /// <summary>(NOT IMPLEMENTED YET) Save all files</summary>
        /// <param name="filePos">File position </param>
        public void Close(int filePos) {
            MediaInfoList_Close(_handle, (IntPtr) filePos);
        }

        /// <summary>Get all details about a file in one string</summary>
        /// <param name="filePos">File position</param>
        /// <remarks>you can know the position in searching the filename with MediaInfoList::Get(FilePos, 0, 0, "CompleteName")</remarks>
        /// <remarks>You can change default presentation with Inform_Set()</remarks>
        /// <returns></returns>
        public String Inform(int filePos) {
            return Marshal.PtrToStringUni(MediaInfoList_Inform(_handle, (IntPtr) filePos, (IntPtr) 0));
        }

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="filePos">File position</param>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <param name="kindOfSearch">Where to look for the parameter</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public String Get(int filePos, StreamKind streamKind, int streamNumber, String parameter, InfoKind kindOfInfo = InfoKind.Text, InfoKind kindOfSearch = InfoKind.Name) {
            return Marshal.PtrToStringUni(MediaInfoList_Get(_handle, (IntPtr) filePos, (IntPtr) streamKind, (IntPtr) streamNumber, parameter, (IntPtr) kindOfInfo, (IntPtr) kindOfSearch));
        }

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="filePos">File position</param>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">arameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public String Get(int filePos, StreamKind streamKind, int streamNumber, int parameter, InfoKind kindOfInfo = InfoKind.Text) {
            return Marshal.PtrToStringUni(MediaInfoList_GetI(_handle, (IntPtr) filePos, (IntPtr) streamKind, (IntPtr) streamNumber, (IntPtr) parameter, (IntPtr) kindOfInfo));
        }

        /// <summary>Configure or get information about MediaInfoLib</summary>
        /// <param name="option">The name of option</param>
        /// <param name="value">The value of option</param>
        /// <returns>Depend of the option: by default "" (nothing) means No, other means Yes</returns>
        public String Option(String option, String value = "") {
            return Marshal.PtrToStringUni(MediaInfoList_Option(_handle, option, value));
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
        public int StateGet() {
            return (int) MediaInfoList_State_Get(_handle);
        }

        /// <summary>Count of streams, or count of piece of information in this stream.</summary>
        /// <param name="filePos">File position</param>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in this kind of stream (first, second...)</param>
        /// <remarks>you can know the position in searching the filename with MediaInfoList::Get(FilePos, 0, 0, "CompleteName")</remarks>
        /// <returns></returns>
        public int CountGet(int filePos, StreamKind streamKind, int streamNumber) {
            return (int) MediaInfoList_Count_Get(_handle, (IntPtr) filePos, (IntPtr) streamKind, (IntPtr) streamNumber);
        }

        /// <summary>Get the count of opened files.</summary>
        /// <param name="filePos">File position</param>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <remarks>you can know the position in searching the filename with MediaInfoList::Get(FilePos, 0, 0, "CompleteName")</remarks>
        /// <returns></returns>
        public int CountGet(int filePos, StreamKind streamKind) {
            return CountGet(filePos, streamKind, -1);
        }

        /// <summary>(NOT IMPLEMENTED YET) Save all files opened before with Open() (modifications of tags)</summary>
        public void Close() {
            Close(-1);
        }

    }
}
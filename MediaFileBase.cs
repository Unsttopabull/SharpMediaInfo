using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Frost.SharpMediaInfo.Options;
using Frost.SharpMediaInfo.Output;

namespace Frost.SharpMediaInfo {

    /// <summary>Represent MediaInfo details about a file.</summary>
    public abstract class MediaFileBase : IDisposable {

        protected IntPtr Handle;
        protected string DisposedMessage;
        protected const string HANDLE_DISPOSED = "A handle to MedaiInfo DLL has already beed disposed.";
        protected const string FILE_CLOSED = "This file has already been closed. Only cached info can be accessed if available.";

        /// <summary>Initializes a new instance of the <see cref="MediaFileBase"/> class.</summary>
        /// <param name="handle">The handle to the MediaInfo DLL.</param>
        protected MediaFileBase(IntPtr handle) {
            Handle = handle;
        }
        
        /// <summary>Initializes the media streams.</summary>
        /// <param name="cacheInfom">If set to <c>true</c> the Iform() call will get cached to limit calls to DLL when cached.</param>
        /// <param name="allInfoCache">If set to <c>true</c> the Inform() call will get cached with ShowAllInfo set to <c>true</c>.</param>
        /// <remarks>Dependant on the derived classes contructor so has to be called there.</remarks>
        protected void InitializeMediaStreams(bool cacheInfom, bool allInfoCache) {
            Info = new LibraryInfo(this);
            Options = new Settings(this);

            General = new MediaGeneral(this);
            Text = new MediaText(this);
            Menu = new MediaMenu(this);
            Other = new MediaOther(this);
            Image = new MediaImage(this);
            Audio = new MediaAudio(this);
            Video = new MediaVideo(this);

            if (IsOpen && cacheInfom) {
                CacheInform(allInfoCache);
            }
        }

        #region Options & Info

        public LibraryInfo Info { get; protected set; }

        public Settings Options { get; protected set; }

        #endregion

        #region Media Properties

        /// <summary>General info about the media file</summary>
        public MediaGeneral General { get; protected set; }

        /// <summary>Info about subtitles and other text elements</summary>
        public MediaText Text { get; protected set; }

        /// <summary>Info about video format, codec, compression ...</summary>
        public MediaVideo Video { get; protected set; }

        /// <summary>Info about audio format, codec, channels, positions</summary>
        public MediaAudio Audio { get; protected set; }

        /// <summary>Info about image resolution, format, width, height, ...</summary>
        public MediaImage Image { get; protected set; }

        /// <summary>Other info that couldn't be put in other categories</summary>
        public MediaOther Other { get; protected set; }

        /// <summary>Info about menues and chapters</summary>
        public MediaMenu Menu { get; protected set; }

        #endregion

        #region Inform parsing/caching

        /// <summary>Gets the XML inform and passes it on then restores the original inform type</summary>
        /// <param name="allInfoInform">If set to <c>true</c> the Inform() call will get cached with ShowAllInfo set to <c>true</c>.</param>
        private void CacheInform(bool allInfoInform) {
            string prevInform = Options.Inform;

            bool showAllInfo = Options.ShowAllInfo;
            if (allInfoInform) {
                Options.ShowAllInfo = true;
            }

            Options.InformPreset = InformPreset.XML;

            string inform = Inform();
            if(!string.IsNullOrEmpty(inform)) {
                ParseInform(inform);
            }

            if (allInfoInform) {
                Options.ShowAllInfo = showAllInfo;
            }

            Options.Inform = prevInform;
        }

        private void ParseInform(string inform) {
            IEnumerable<XNode> xNodes;
            try {
                xNodes = ((XElement) XDocument.Load(new StringReader(inform)).FirstNode).Nodes();
            }
            catch (Exception) {
                Console.Error.WriteLine("Error while parshing/caching MediaInfo Inform.");
                Console.Error.WriteLine(inform);
                return;
            }

            //stream number counters for every stream kind (video, audio, text, menu ...)
            int[] counters = new int[7];

            foreach (XElement track in xNodes) {
                XAttribute trackType = track.FirstAttribute;
                if (trackType == null) {
                    continue;
                }

                string type = trackType.Value;

                StreamKind streamKind;
                if (!Enum.TryParse(type, true, out streamKind)) {
                    continue;
                }

                switch (streamKind) {
                    case StreamKind.General:
                        General.ParseInform(track, counters[(int) StreamKind.General]++);
                        break;
                    case StreamKind.Video:
                        Video.ParseInform(track, counters[(int) StreamKind.Video]++);
                        break;
                    case StreamKind.Audio:
                        Audio.ParseInform(track, counters[(int) StreamKind.Audio]++);
                        break;
                    case StreamKind.Text:
                        Text.ParseInform(track, counters[(int) StreamKind.Text]++);
                        break;
                    case StreamKind.Other:
                        Other.ParseInform(track, counters[(int) StreamKind.Other]++);
                        break;
                    case StreamKind.Image:
                        Image.ParseInform(track, counters[(int) StreamKind.Image]++);
                        break;
                    case StreamKind.Menu:
                        Menu.ParseInform(track, counters[(int) StreamKind.Menu]++);
                        break;
                }
            }
        }

        #endregion

        #region IDisposable

        /// <summary>Throws if the handle to the MediaInfo DLL has already been disposed.</summary>
        /// <exception cref="System.ObjectDisposedException">Throws if a handle to MedaiInfo DLL has already beed disposed.</exception>
        protected void ThrowIfDisposed() {
            if (!IsOpen) {
                throw new ObjectDisposedException(GetType().FullName, DisposedMessage);
            }
        }

        /// <summary>Gets or sets a value indicating whether this file is open.</summary>
        /// <value>Is <c>true</c> if the file is open; otherwise, <c>false</c>.</value>
        public bool IsOpen { get; internal set; }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        void IDisposable.Dispose() {
            Dispose(false);
        }

        ///// <summary>Closes this instance and disposes all allocated resources.</summary>
        //internal virtual void Close() {
        //    throw new NotImplementedException("Should be overriden in a inherited class.");
        //}

        protected abstract void Dispose(bool destructor);

        ~MediaFileBase() {
            Dispose(true);
        }

        #endregion

        #region MediaInfo Interop

        /// <summary>Get all details about a file in one string</summary>
        /// <returns>string with all the file details</returns>
        /// <remarks>You can change default presentation with Inform_Set()</remarks>
        public abstract string Inform();

        /// <summary>Configure or get information about MediaInfoLib</summary>
        /// <param name="option">The option.</param>
        /// <param name="value">The value of option</param>
        /// <returns>Depend of the option: by default "" (nothing) means No, other means Yes</returns>
        internal abstract string Option(string option, string value = "");

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
        public abstract int StateGet();

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in string format ("Codec", "Width"...) </param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <param name="kindOfSearch">Where to look for the parameter</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public abstract string Get(StreamKind streamKind, int streamNumber, string parameter, InfoKind kindOfInfo = InfoKind.Text, InfoKind kindOfSearch = InfoKind.Name);

        /// <summary>Get a piece of information about a file (parameter is an integer)</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in Kind of stream (first, second...)</param>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public abstract string Get(StreamKind streamKind, int streamNumber, int parameter, InfoKind kindOfInfo = InfoKind.Text);

        /// <summary>Count of streams of a stream kind or count of piece of information in this stream.</summary>
        /// <param name="streamKind">Kind of stream (general, video, audio...)</param>
        /// <param name="streamNumber">Stream number in this kind of stream (first, second...)</param>
        /// <returns></returns>
        public abstract int CountGet(StreamKind streamKind, int streamNumber = -1);

        #endregion
    }

}
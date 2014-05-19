using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Frost.SharpMediaInfo.Output.Properties.Codecs;

#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output {

    public abstract class Media {

        private readonly MediaFileBase _mediaFile;
        private readonly StreamKind _streamKind;
        private readonly Dictionary<string, string>[] _properties;
        private readonly bool[] _cached;
        protected int CachedStreamCount;

        protected Media(MediaFileBase mediaInfo, StreamKind streamKind) {
            _mediaFile = mediaInfo;
            _streamKind = streamKind;
            StreamNumber = 0;

            CachedStreamCount = -1;
            int count = Count;
            _properties = new Dictionary<string, string>[count];
            for (int streamNum = 0; streamNum < count; streamNum++) {
                _properties[streamNum] = new Dictionary<string, string>();
            }

            _cached = new bool[count];

            CodecIDInfo = new CodecID(this);
        }

        #region Properties

        /// <summary>Bit field (0=IsAccepted, 1=IsFilled, 2=IsUpdated, 3=IsFinished)</summary>
        public string Status { get { return this["Status"]; } }

        /// <summary>Number of the stream kind stream (base=0)</summary>
        /// <example>\eg{ <c>1</c> for a second stream of this type.}</example>
        public string StreamKindID { get { return this["StreamKindID"]; } }

        /// <summary>When multiple streams, number of the stream (base=1)</summary>
        /// <example>\eg{ <c>1</c> for a first stream of this type.}</example>
        public string StreamKindPos { get { return this["StreamKindPos"]; } }

        /// <summary>Stream order in the file, whatever is the kind of stream (base=0).</summary>
        public long? StreamOrder { get { return TryParseLong("StreamOrder"); } }

        public long? FirstPacketOrder { get { return TryParseLong("FirstPacketOrder"); } }

        public long? ID { get { return TryParseLong("ID"); } }

        public string IDString { get { return this["IDString"]; } }

        public long? UniqueID { get { return TryParseLong("UniqueID"); } }

        public string UniqueIDString { get { return this["UniqueID/String"]; } }

        public long? MenuID { get { return TryParseLong("MenuID"); } }

        public string MenuIDString { get { return this["MenuID/String"]; } }

        /// <summary>The default stream number to use when accessing media info through properties</summary>
        public int StreamNumber { get; set; }

        /// <summary>The number of <see cref="Frost.SharpMediaInfo.StreamKind">StreamKind</see> streams.</summary>
        public int Count {
            get {
                if (CachedStreamCount == -1) {
                    CachedStreamCount = _mediaFile.CountGet(_streamKind);
                }
                return CachedStreamCount;
            }
        }

        public CodecID CodecIDInfo { get; private set; }

        /// <summary>Gets a value indicating whether information exists about this kind of stream</summary>
        /// <value>Is <c>true</c> if info is available; otherwise, <c>false</c>.</value>
        /// <remarks>Equivalent to checking if StreamCount is equal to 0</remarks>
        public bool Any {
            get { return Count != 0; }
        }

        #endregion

        #region Indexers

        /// <summary>Get a piece of information about a media element</summary>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in string format ("Codec", "Width"...) </param>
        public string this[string parameter] {
            get {
                //if caching is enabled and the parameter exists in cache return cached value
                if (_cached.Length > 0 && _cached[StreamNumber] && _properties[StreamNumber].ContainsKey(parameter)) {
                    return _properties[StreamNumber][parameter];
                }

                //otherwise call MediaInfo DLL
                string value = _mediaFile.Get(_streamKind, StreamNumber, parameter);

                //if the returned string is not empty or null we return the value (and optional cache it)
                if (!string.IsNullOrEmpty(value)) {
                    if (_cached[StreamNumber]) {
                        //we tested if the dictionary already contains this key
                        //so no need to check again
                        _properties[StreamNumber].Add(parameter, value);
                    }
                    return value;
                }
                //otherwise we return null
                return null;
            }
        }

        /// <summary>Get a piece of information about a media element (parameter is an integer)</summary>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        public string this[int parameter] {
            get {
                string str = _mediaFile.Get(_streamKind, StreamNumber, parameter);
                return string.IsNullOrEmpty(str)
                    ? null
                    : str;
            }
        }

        #endregion

        #region Caching / Inform Parsing

        /// <summary>Clears the cache all the cached values of this stream and optionaly all steams of this kind.</summary>
        /// <param name="allStreamsOfThisType">If set to <c>true</c> clears cache for all the streams of this kind.</param>
        public void ClearCache(bool allStreamsOfThisType = false) {
            if (allStreamsOfThisType) {
                foreach (Dictionary<string, string> dictionary in _properties) {
                    dictionary.Clear();
                }
                return;
            }
            _properties[StreamNumber].Clear();
        }

        /// <summary>Gets the all the cached values as a key-value pair with the key as the xml MediaInfo property key.</summary>
        /// <value>All the cached values as a key-value pair with the key as the xml MediaInfo property key.</value>
        public IEnumerable<KeyValuePair<string, string>> CachedValues {
            get { return _properties[StreamNumber]; }
        }

        internal void ParseInform(XElement track, int streamNumber) {
            if (_cached[streamNumber]) {
                return;
            }

            if (track == null) {
                return;
            }

            Dictionary<string, int> multipleOccurances = new Dictionary<string, int>();
            foreach (XElement xElement in track.Nodes()) {
                string tagName = xElement.Name.LocalName;

                //check if this tag has already occured in this stream
                if (_properties[streamNumber].ContainsKey(tagName)) {
                    //if it has and its already in the multiple occuraces dictionary
                    //we just increment the number of occurances and append it to the tag name
                    if (multipleOccurances.ContainsKey(tagName)) {
                        tagName = string.Format("{0}/String{1}", tagName, ++multipleOccurances[tagName]);
                    }
                    else {
                        //otherwise we add new entry to the dictionary and add "/String" to the tag name
                        multipleOccurances.Add(tagName, 0);
                        tagName += "/String";
                    }
                }

                _properties[streamNumber][tagName] = xElement.Value;
            }
            _cached[streamNumber] = true;
        }
        #endregion

        #region MediaInfo Getters & Functions

        /// <summary>Get a piece of information about an image</summary>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in string format ("Codec", "Width"...) </param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <param name="kindOfSearch">Where to look for the parameter</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public string Get(string parameter, InfoKind kindOfInfo = InfoKind.Text, InfoKind kindOfSearch = InfoKind.Name) {
            return _mediaFile.Get(_streamKind, StreamNumber, parameter, kindOfInfo, kindOfSearch);
        }

        /// <summary>Get a piece of information about an image (parameter is an integer)</summary>
        /// <param name="parameter">Parameter you are looking for in the stream (Codec, width, bitrate...), in integer format (first parameter, second parameter...)</param>
        /// <param name="kindOfInfo">Kind of information you want about the parameter (the text, the measure, the help...)</param>
        /// <returns>a string about information you search, an empty string if there is a problem</returns>
        public string Get(int parameter, InfoKind kindOfInfo) {
            return _mediaFile.Get(_streamKind, StreamNumber, parameter, kindOfInfo);
        }

        #endregion

        #region Type parsers

        public long? TryParseLong(string parameter) {
            int value;
            if (int.TryParse(this[parameter], NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                return value;
            }
            return null;
        }

        public float? TryParseFloat(string parameter) {
            float value;
            if (float.TryParse(this[parameter], NumberStyles.Float, CultureInfo.InvariantCulture, out value)) {
                return value;
            }
            return null;
        }

        public TimeSpan? TryParseTimeSpan(string parameter) {
            long? miliseconds = TryParseLong(parameter);
            if (miliseconds.HasValue) {
                return TimeSpan.FromMilliseconds(miliseconds.Value);
            }
            return null;
        }

        public CompressionMode ParseCompressionMode(string parameter) {
            switch (this[parameter]) {
                case "Lossy":
                    return CompressionMode.Lossy;
                case "Lossless":
                    return CompressionMode.Lossless;
                default:
                    return CompressionMode.Unknown;
            }
        }

        public byte[] ParseBase64String(string parameter) {
            string enc = this[parameter];

            return enc == null
                       ? null
                       : Convert.FromBase64String(enc);
        }

        public string[] ParseStringList(string parameter, string separator = " / ") {
            string list = this[parameter];

            //can only test for null as the indexer already checks for string.IsNullOrEmpty() and
            //coerces empty strings to null
            return list != null
                ? list.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                : null;
        }

        public bool? TryParseBool(string parameter) {
            bool value;
            if (bool.TryParse(this[parameter], out value)) {
                return value;
            }
            return null;
        }

        public DateTime? TryParseDateTime(string parameter, bool utc) {
            string dateFormat = utc
                ? "UTC yyyy-MM-dd hh:mm:ss.fff"
                : "yyyy-MM-dd hh:mm:ss.fff";

            DateTimeStyles dateTimeStyles = utc
                ? (DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal)
                : DateTimeStyles.AllowWhiteSpaces;

            DateTime dt;
            if (DateTime.TryParseExact(this[parameter], dateFormat, CultureInfo.InvariantCulture, dateTimeStyles, out dt)) {
                return dt;
            }
            return null;
        }

        #endregion

        ///// <summary>Returns a string that represents the current object.</summary>
        ///// <returns>A string that represents the current object.</returns>
        //public override string ToString() {
        //    StringBuilder sb = new StringBuilder(10000);
        //    foreach (KeyValuePair<string, string> kvp in _properties[StreamNumber]) {
        //        sb.AppendLine(kvp.Key + " : " + kvp.Value);
        //    }
        //    return sb.ToString();
        //}

    }

}
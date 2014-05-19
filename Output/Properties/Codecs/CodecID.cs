namespace Frost.SharpMediaInfo.Output.Properties.Codecs {
    public class CodecID {
        private readonly Media _media;

        internal CodecID(Media media) {
            _media = media;
        }

        /// <summary>Codec ID (found in some containers)</summary>
        public string ID { get { return _media["CodecID"]; } }

        /// <summary>Codec ID (found in some containers)</summary>
        public string String { get { return _media["CodecID/String"]; } }

        /// <summary>Info about codec ID</summary>
        public string Info { get { return _media["CodecID/Info"]; } }

        /// <summary>A hint for this codec ID</summary>
        public string Hint { get { return _media["CodecID/Hint"]; } }

        /// <summary>A link for more details about this codec ID</summary>
        public string Url { get { return _media["CodecID/Url"]; } }

        /// <summary>Manual description given by the container</summary>
        public string Description { get { return _media["CodecID_Description"]; } }
    }
}
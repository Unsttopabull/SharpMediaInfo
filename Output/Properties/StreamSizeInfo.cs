namespace Frost.SharpMediaInfo.Output.Properties {

    public enum StreamSizeType {
        StreamSize,
        SourceStreamSize,
        EncodedStreamSize,
        EncodedSourceStreamSize
    }

    public class StreamSizeInfo {

        private readonly Media _media;
        private readonly string[] _propNames;

        internal StreamSizeInfo(Media mediaAudio, StreamSizeType type = StreamSizeType.StreamSize) {
            _media = mediaAudio;

            switch (type) {
                case StreamSizeType.StreamSize:
                    _propNames = new[] { "StreamSize/String", "StreamSize/String1", "StreamSize/String2", "StreamSize/String3", "StreamSize/String4", "StreamSize/String5", "StreamSize_Proportion" };
                    break;
                case StreamSizeType.SourceStreamSize:
                    _propNames = new[] { "Source_StreamSize/String", "Source_StreamSize/String1", "Source_StreamSize/String2", "Source_StreamSize/String3", "Source_StreamSize/String4", "Source_StreamSize/String", "Source_StreamSize_Proportion" };
                    break;
                case StreamSizeType.EncodedStreamSize:
                    _propNames = new[] { "StreamSize_Encoded/String", "StreamSize_Encoded/String1", "StreamSize_Encoded/String2", "StreamSize_Encoded/Strin3", "StreamSize_Encoded/Strin4", "StreamSize_Encoded/String5", "StreamSize_Encoded_Proportion" };
                    break;
                case StreamSizeType.EncodedSourceStreamSize:
                    _propNames = new[] { "Source_StreamSize_Encoded/String", "Source_StreamSize_Encoded/String1", "Source_StreamSize_Encoded/String2", "Source_StreamSize_Encoded/String3", "Source_StreamSize_Encoded/String4", "Source_StreamSize_Encoded/String5", "Source_StreamSize_Encoded_Proportion" };
                    break;
            }

        }

        /// <summary>Streamsize in with percentage value</summary>
        public string String { get { return _media[_propNames[0]]; } }
        public string String1 { get { return _media[_propNames[1]]; } }
        public string String2 { get { return _media[_propNames[2]]; } }
        public string String3 { get { return _media[_propNames[3]]; } }
        public string String4 { get { return _media[_propNames[4]]; } }

        /// <summary>Streamsize in with percentage value</summary>
        public string String5 { get { return _media[_propNames[5]]; } }

        /// <summary>Stream size divided by file size</summary>
        public float? Proportion { get { return _media.TryParseLong(_propNames[6]); } }
    }
}
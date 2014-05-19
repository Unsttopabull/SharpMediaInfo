namespace Frost.SharpMediaInfo.Output.Properties {

    public enum SizeType {
        Width,
        Height
    }

    public class SizeInfo {
        private readonly Media _media;
        private readonly string[] _propNames;

        public SizeInfo(Media media, SizeType type) {
            _media = media;

            switch (type) {
                case SizeType.Width:
                    _propNames = new[] { "Width/String", "Width_Offset", "Width_Offset/String", "Width_Original", "Width_Original/String" };
                    break;
                case SizeType.Height:
                    _propNames = new[] { "Height/String", "Height_Offset", "Height_Offset/String", "Height_Original", "Height_Original/String" };
                    break;
            }
        }

        /// <summary>Size (aperture size if present) with measurement (pixel)</summary>
        public string String { get { return _media[_propNames[0]]; } }

        /// <summary>Offset between original height and displayed size (aperture size) in pixel</summary>
        public long? Offset { get { return _media.TryParseLong(_propNames[1]); } }
        /// <summary>Offset between original height and displayed size (aperture size) in pixel</summary>
        public string OffsetString { get { return _media[_propNames[2]]; } }

        /// <summary>Original (in the raw stream) size in pixel</summary>
        public long? Original { get { return _media.TryParseLong(_propNames[3]); } }
        /// <summary>Original (in the raw stream) size with measurement (pixel)</summary>
        public string OriginalString { get { return _media[_propNames[4]]; } }
    }
}
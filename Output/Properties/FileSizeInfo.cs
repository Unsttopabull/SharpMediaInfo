namespace Frost.SharpMediaInfo.Output.Properties {
    public class FileSizeInfo {
        private readonly Media _media;

        public FileSizeInfo(Media media) {
            _media = media;
        }

        /// <summary>File size (with measure)</summary>
        public string String { get { return _media["FileSize/String"]; } }

        /// <summary>File size (with measure, 1 digit mini)</summary>
        public string String1 { get { return _media["FileSize/String1"]; } }

        /// <summary>File size (with measure, 2 digit mini)</summary>
        public string String2 { get { return _media["FileSize/String2"]; } }

        /// <summary>File size (with measure, 3 digit mini)</summary>
        public string String3 { get { return _media["FileSize/String3"]; } }

        /// <summary>File size (with measure, 4 digit mini)</summary>
        public string String4 { get { return _media["FileSize/String4"]; } }
    }
}
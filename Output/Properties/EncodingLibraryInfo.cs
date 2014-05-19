namespace Frost.SharpMediaInfo.Output.Properties {
    public class EncodingLibraryInfo {
        private readonly Media _media;

        public EncodingLibraryInfo(Media media) {
            _media = media;
        }

        /// <summary>Software used to create the file</summary>
        public string String { get { return _media["Encoded_Library/String"]; } }

        /// <summary>Info from the software</summary>
        public string Name { get { return _media["Encoded_Library/Name"]; } }

        /// <summary>Version of software</summary>
        public string Version { get { return _media["Encoded_Library/Version"]; } }

        /// <summary>Release date of software</summary>
        public string Date { get { return _media["Encoded_Library/Date"]; } }

        /// <summary>Parameters used by the software</summary>
        public string[] Settings { get { return _media.ParseStringList("Encoded_Library_Settings"); } }
    }
}
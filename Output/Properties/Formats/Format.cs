
namespace Frost.SharpMediaInfo.Output.Properties.Formats {

    public class Format {

        protected readonly Media MediaStream;

        internal Format(Media media) {
            MediaStream = media;
        }

        /// <summary>Format used</summary>
        public string Name { get { return MediaStream["Format"]; } }

        /// <summary>Info about the format</summary>
        public string Info { get { return MediaStream["Format/Info"]; } }

        /// <summary>Homepage of this format</summary>
        public string Url { get { return MediaStream["Format/Url"]; } }

        /// <summary>Commercial name used by vendor for theses setings or Format field if there is no difference</summary>
        public string Commercial { get { return MediaStream["Format_Commercial"]; } }

        /// <summary>Commercial name used by vendor for theses setings if there is one</summary>
        public string CommercialIfAny { get { return MediaStream["Format_Commercial_IfAny"]; } }

        /// <summary>Version of this format</summary>
        public string Version { get { return MediaStream["Format_Version"]; } }

        /// <summary>Profile of this Format</summary>
        public string Profile { get { return MediaStream["Format_Profile"]; } }

        /// <summary>Compression method used</summary>
        public string Compression { get { return MediaStream["Format_Compression"]; } }

        /// <summary>Settings needed for decoder used, summary</summary>
        public string[] Settings { get { return MediaStream.ParseStringList("Format_Settings"); } }
    }
}
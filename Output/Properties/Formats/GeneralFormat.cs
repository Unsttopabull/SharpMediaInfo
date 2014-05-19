namespace Frost.SharpMediaInfo.Output.Properties.Formats {

    public class GeneralFormat : Format {

        internal GeneralFormat(Media media) : base(media) {
        }

        public string String { get { return MediaStream["Format/String"]; } }

        public string[] FormatExtensions { get { return MediaStream.ParseStringList("Format/Extensions", " "); } }

    }
}
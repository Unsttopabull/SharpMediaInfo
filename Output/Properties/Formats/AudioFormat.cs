#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output.Properties.Formats {
    public class AudioFormat : Format {
        internal AudioFormat(Media media) : base(media) {
        }

        public string SBR { get { return MediaStream["Format_Settings_SBR"]; } }
        public string SBRString { get { return MediaStream["Format_Settings_SBR/String"]; } }

        public string PS { get { return MediaStream["Format_Settings_PS"]; } }
        public string PSString { get { return MediaStream["Format_Settings_PS/String"]; } }

        public string Mode { get { return MediaStream["Format_Settings_Mode"]; } }
        public string ModeExtension { get { return MediaStream["Format_Settings_ModeExtension"]; } }

        public string Emphasis { get { return MediaStream["Format_Settings_Emphasis"]; } }
        public string Floor { get { return MediaStream["Format_Settings_Floor"]; } }
        public string Firm { get { return MediaStream["Format_Settings_Firm"]; } }
        public string Endianness { get { return MediaStream["Format_Settings_Endianness"]; } }
        public string Sign { get { return MediaStream["Format_Settings_Sign"]; } }
        public string Law { get { return MediaStream["Format_Settings_Law"]; } }
        public string ITU { get { return MediaStream["Format_Settings_ITU"]; } }

        /// <summary>Wrapping mode (Frame wrapped or Clip wrapped)</summary>
        public string Wrapping { get { return MediaStream["Format_Settings_Wrapping"]; } }
    }
}
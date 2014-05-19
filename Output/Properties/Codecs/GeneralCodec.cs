namespace Frost.SharpMediaInfo.Output.Properties.Codecs {

    public class GeneralCodec : Codec {

        internal GeneralCodec(Media mediaMenu) : base(mediaMenu) {
        }

        public string[] Extensions { get { return MediaStream.ParseStringList("Codec/Extensions", " "); } }
        public string[] Settings { get { return MediaStream.ParseStringList("Codec_Settings"); } }
        public string SettingsAutomatic { get { return MediaStream["Codec_Settings_Automatic"]; } }
    }
}
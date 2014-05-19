#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output.Properties.Codecs {

    public class AudioCodecSettings {

        private readonly Media _media;

        internal AudioCodecSettings(Media media) {
            _media = media;
        }

        public string[] Summary { get { return _media.ParseStringList("Codec_Settings"); } }
        public string Automatic { get { return _media["Codec_Settings_Automatic"]; } }
        public string Floor { get { return _media["Codec_Settings_Floor"]; } }
        public string Firm { get { return _media["Codec_Settings_Firm"]; } }
        public string Endianness { get { return _media["Codec_Settings_Endianness"]; } }
        public string Sign { get { return _media["Codec_Settings_Sign"]; } }
        public string Law { get { return _media["Codec_Settings_Law"]; } }

        public string ITU { get { return _media["Codec_Settings_ITU"]; } }
    }
}
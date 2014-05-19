namespace Frost.SharpMediaInfo.Output.Properties.Codecs {
    public class AudioCodec : TextCodec {

        public AudioCodec(Media media) : base(media) {
            Settings = new AudioCodecSettings(media);
        }

        public string Family { get { return MediaStream["Codec/Family"]; } }
        public string Description { get { return MediaStream["Codec_Description"]; } }
        public string Profile { get { return MediaStream["Codec_Profile"]; } }

        public AudioCodecSettings Settings { get; private set; }
    }
}
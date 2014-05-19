
namespace Frost.SharpMediaInfo.Output.Properties.Codecs {

    public class VideoCodec : TextCodec {

        public VideoCodec(Media media) : base(media) {
            Settings = new VideoCodecSettings(media);
        }

        public string Family { get { return MediaStream["Codec/Family"]; } }
        public string Profile { get { return MediaStream["Codec_Profile"]; } }
        public string Description { get { return MediaStream["Codec_Description"]; } }

        public VideoCodecSettings Settings { get; private set; }

    }
}
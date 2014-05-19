namespace Frost.SharpMediaInfo.Output.Properties.Codecs {

    public class VideoCodecSettings {

        private readonly Media _media;

        public VideoCodecSettings(Media media) {
            _media = media;
        }

        public string[] Summary { get { return _media.ParseStringList("Codec_Settings"); } }

        public bool? PackedBitStream { get { return _media.TryParseBool("Codec_Settings_PacketBitStream"); } }

        public string BVOP { get { return _media["Codec_Settings_BVOP"]; } }

        public bool? QPel { get { return _media.TryParseBool("Codec_Settings_QPel"); } }

        public long? GMC { get { return _media.TryParseLong("Codec_Settings_GMC"); } }
        public string GMCString { get { return _media["Codec_Settings_GMC/String"]; } }

        /// <summary>Gets the matrix used when encoding.</summary>
        /// <value>The matrix used when encoding.</value>
        public string Matrix { get { return _media["Codec_Settings_Matrix"]; } }
        public byte[] MatrixData { get { return _media.ParseBase64String("Codec_Settings_Matrix_Data"); } }

        public bool? CABAC { get { return _media.TryParseBool("Codec_Settings_CABAC"); } }

        public long? RefFrames { get { return _media.TryParseLong("Codec_Settings_RefFrames"); } }

    }

}
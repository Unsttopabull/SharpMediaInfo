namespace Frost.SharpMediaInfo.Output.Properties.Formats {

    public class VideoFormat : Format {

        internal VideoFormat(Media media) : base(media) {
        }

        public string BVOP { get { return MediaStream["Format_Settings_BVOP"]; } }
        public string BVOPString { get { return MediaStream["Format_Settings_BVOP/String"]; } }

        public bool? QPel { get { return MediaStream.TryParseBool("Format_Settings_QPel"); } }
        public string QPelString { get { return MediaStream["Format_Settings_QPel/String"]; } }

        public long? GMC { get { return MediaStream.TryParseLong("Format_Settings_GMC"); } }
        public string GMCString { get { return MediaStream["Format_Settings_GMC/String"]; } }

        public string Matrix { get { return MediaStream["Format_Settings_Matrix"]; } }
        public string MatrixString { get { return MediaStream["Format_Settings_Matrix/String"]; } }

        /// <summary>Matrix, in binary format encoded BASE64. Order = intra, non-intra, gray intra, gray non-intra</summary>
        public byte[] MatrixData { get { return MediaStream.ParseBase64String("Format_Settings_Matrix_Data"); } }

        public bool? CABAC { get { return MediaStream.TryParseBool("Format_Settings_CABAC"); } }
        public string CABACString { get { return MediaStream["Format_Settings_CABAC/String"]; } }

        public long? RefFrames { get { return MediaStream.TryParseLong("Format_Settings_RefFrames"); } }
        public string RefFramesString { get { return MediaStream["Format_Settings_RefFrames/String"]; } }

        public string Pulldown { get { return MediaStream["Format_Settings_Pulldown"]; } }

        public string FrameMode { get { return MediaStream["Format_Settings_FrameMode"]; } }

        /// <summary>detailled (M=x N=y)</summary>
        public string GOP { get { return MediaStream["Format_Settings_GOP"]; } }

        public string FrameStructures { get { return MediaStream["Format_Settings_FrameStructures"]; } }

        /// <summary>Wrapping mode (Frame wrapped or Clip wrapped)</summary>
        public string Wrapping { get { return MediaStream["Format_Settings_Wrapping"]; } }
    }
}
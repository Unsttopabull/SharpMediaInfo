
namespace Frost.SharpMediaInfo.Output.Properties.FrameRate {

    public class FrameRateInfo {
        protected readonly Media Media;

        public FrameRateInfo(Media media) {
            Media = media;
        }

        /// <summary>Frames per second (with measurement)</summary>
        public string String { get { return Media["FrameRate/String"]; } }

        /// <summary>Original (in the raw stream) frames per second</summary>
        public float? Original { get { return Media.TryParseFloat("FrameRate_Original"); } }
        /// <summary>Original (in the raw stream) frames per second</summary>
        public string OriginalString { get { return Media["FrameRate_Original/String"]; } }

        /// <summary>Frame rate mode (CFR, VFR)</summary>
        public FrameOrBitRateMode Mode {
            get {
                switch (Media["FrameRate_Mode"]) {
                    case "VBR":
                        return FrameOrBitRateMode.Variable;
                    case "CFR":
                        return FrameOrBitRateMode.Constant;
                    default:
                        return FrameOrBitRateMode.Unknown;
                }
            }
        }
        /// <summary>Frame rate mode (Constant, Variable)</summary>
        public string ModeString { get { return Media["FrameRate_Mode/String"]; } }

        /// <summary>Minimum Frames per second</summary>
        public float? Minimum { get { return Media.TryParseFloat("FrameRate_Minimum"); } }
        /// <summary>Minimum Frames per second (with measurement)</summary>
        public string MinimumString { get { return Media["FrameRate_Minimum/String"]; } }

        /// <summary>Nominal Frames per second</summary>
        public float? Nominal { get { return Media.TryParseFloat("FrameRate_Nominal"); } }
        /// <summary>Nominal Frames per second (with measurement)</summary>
        public string NominalString { get { return Media["FrameRate_Nominal/String"]; } }

        /// <summary>Maximum Frames per second</summary>
        public float? Maximum { get { return Media.TryParseFloat("FrameRate_Maximum"); } }
        /// <summary>Maximum Frames per second (with measurement)</summary>
        public string MaximumString { get { return Media["FrameRate_Maximum/String"]; } }
    }
}
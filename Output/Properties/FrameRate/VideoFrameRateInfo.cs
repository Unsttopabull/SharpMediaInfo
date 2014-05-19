
namespace Frost.SharpMediaInfo.Output.Properties.FrameRate {

    public class VideoFrameRateInfo : FrameRateInfo {

        public VideoFrameRateInfo(Media media) : base(media) {
        }

        /// <summary>Original frame rate mode (CFR, VFR)</summary>
        public FrameOrBitRateMode ModeOriginal {
            get {
                switch (Media["FrameRate_Mode_Original"]) {
                    case "VBR":
                        return FrameOrBitRateMode.Variable;
                    case "CFR":
                        return FrameOrBitRateMode.Constant;
                    default:
                        return FrameOrBitRateMode.Unknown;
                }
            }
        }

        /// <summary>Original frame rate mode (Constant, Variable)</summary>
        public string ModeOriginalString { get { return Media["FrameRate_Mode_Original/String"]; } }

    }

}
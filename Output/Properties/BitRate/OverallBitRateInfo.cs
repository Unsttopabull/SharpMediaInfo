namespace Frost.SharpMediaInfo.Output.Properties.BitRate {

    public class OverallBitRateInfo {
        protected readonly Media MediaStream;
        private readonly bool _general;

        internal OverallBitRateInfo(Media media, bool general = true) {
            MediaStream = media;
            _general = general;
        }

        /// <summary>Bit rate (with measurement)</summary>
        public string String { get { return _general ? MediaStream["OverallBitRate/String"] : MediaStream["BitRate/String"]; } }

        /// <summary>Bit rate mode (VBR, CBR)</summary>
        public FrameOrBitRateMode Mode {
            get {
                string mode = _general ? MediaStream["OverallBitRate_Mode"] : MediaStream["BitRate_Mode"];
                switch (mode) {
                    case "VBR":
                        return FrameOrBitRateMode.Variable;
                    case "CFR":
                        return FrameOrBitRateMode.Constant;
                    default:
                        return FrameOrBitRateMode.Unknown;
                }
            }
        }
        /// <summary>Bit rate mode (Constant, Variable)</summary>
        public string ModeString { get { return _general ? MediaStream["OverallBitRate_Mode/String"] : MediaStream["BitRate_Mode/String"]; } }

        /// <summary>Minimum Bit rate in bps</summary>
        public float? Minimum { get { return _general ? MediaStream.TryParseFloat("OverallBitRate_Minimum") : MediaStream.TryParseFloat("BitRate_Minimum"); } }
        /// <summary>Minimum Bit rate (with measurement)</summary>
        public string MinimumString { get { return _general ? MediaStream["OverallBitRate_Minimum/String"] : MediaStream["BitRate_Minimum/String"]; } }

        /// <summary>Nominal Bit rate in bps</summary>
        public float? Nominal { get { return _general ? MediaStream.TryParseFloat("OverallBitRate_Nominal") : MediaStream.TryParseFloat("BitRate_Nominal"); } }
        /// <summary>Nominal Bit rate (with measurement)</summary>
        public string NominalString { get { return _general ? MediaStream["OverallBitRate_Nominal/String"] : MediaStream["BitRate_Nominal/String"]; } }

        /// <summary>Maximum Bit rate in bps</summary>
        public float? Maximum { get { return _general ? MediaStream.TryParseFloat("OverallBitRate_Maximum") : MediaStream.TryParseFloat("BitRate_Maximum"); } }
        /// <summary>Maximum Bit rate (with measurement)</summary>
        public string MaximumString { get { return _general ? MediaStream["OverallBitRate_Maximum/String"] : MediaStream["BitRate_Maximum/String"]; } }
    }
}
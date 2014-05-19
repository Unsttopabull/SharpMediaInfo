namespace Frost.SharpMediaInfo.Options {
    public class MpegTS {
        private readonly MediaFileBase _mf;

        internal MpegTS(MediaFileBase mf) {
            _mf = mf;
        }

        public string MaximumOffset {
            get { return _mf.Option("mpegts_maximumoffset_get"); }
            set { _mf.Option("mpegts_maximumoffset", value); }
        }

        public string MaximumScanDuration {
            get { return _mf.Option("mpegts_maximumscanduration_get"); }
            set { _mf.Option("mpegts_maximumscanduration", value); }
        }

        public double VbrDetectionDelta {
            get {
                double delta;
                double.TryParse(_mf.Option("mpegts_vbrdetection_delta_get"), out delta);

                return delta;
            }
            set { _mf.Option("mpegts_vbrdetection_delta", value.ToString()); }
        }

        public long VbrDetectionOccurences {
            get {
                long numOccurences;
                long.TryParse(_mf.Option("mpegts_vbrdetection_occurences_get"), out numOccurences);
                return numOccurences;
            }
            set { _mf.Option("mpegts_vbrdetection_occurences", value.ToString()); }
        }

        public bool VbrDetectionGiveUp {
            get { return _mf.Option("mpegts_vbrdetection_giveup_get") == "1"; }
            set {
                _mf.Option("mpegts_vbrdetection_giveup", value ? "1" : "0");
            }
        }
    
        public bool ForceStreamDisplay {
            get { return _mf.Option("mpegts_forcestreamdisplay_get") == "1"; }
            set { _mf.Option("mpegts_forcestreamdisplay", value ? "1" : "0"); }
        }
    }
}
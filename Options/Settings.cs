using System;
using System.Globalization;

namespace Frost.SharpMediaInfo.Options {

    public class Settings {

        private readonly MediaFileBase _mf;

        internal Settings(MediaFileBase mf) {
            _mf = mf;

            MpegTS = new MpegTS(_mf);
            SSH = new SSH(_mf);
            SSL = new SSL(_mf);
        }

        public SSH SSH { get; private set; }

        public SSL SSL { get; private set; }

        public MpegTS MpegTS { get; private set; }

        public BlockMethod BlockMethod {
            get {
                return _mf.Option("blockmethod_get") == "1"
                    ? BlockMethod.AfterLocalInfo
                    : BlockMethod.Immediately;
            }
            set {
                _mf.Option("blockmethod", value == BlockMethod.Immediately ? "" : "1");
            }
        }

        public bool ShowAllInfo {
            get { return _mf.Option("Complete_Get") == "1"; }
            set { _mf.Option("Complete", value ? "1" : ""); }
        }

        public bool ParseUnknownExtensions {
            get { return _mf.Option("ParseUnknownExtensions_Get") == "1"; }
            set { _mf.Option("ParseUnknownExtensions", value ? "1" : ""); }
        }

        public bool ReadByHuman {
            get { return _mf.Option("readbyhuman_get") == "1"; }
            set {
                _mf.Option("readbyhuman", value ? "1" : "0");
            }
        }

        public bool LegacyStreamDisplay {
            get { return _mf.Option("LegacyStreamDisplay_get") == "1"; }
            set {
                _mf.Option("LegacyStreamDisplay", value ? "1" : "0");
            }
        }

        public bool SkipBinaryData {
            get { return _mf.Option("SkipBinaryData_get") == "1"; }
            set {
                _mf.Option("SkipBinaryData", value ? "1" : "0");
            }
        }

        public float ParseSpeed {
            get {
                float parseSpeed;
                float.TryParse(_mf.Option("parsespeed_get"), out parseSpeed);

                return parseSpeed;
            }
            set { _mf.Option("parsespeed", value.ToString(CultureInfo.InvariantCulture)); }
        }

        public float Verbosity {
            get {
                float parseSpeed;
                float.TryParse(_mf.Option("verbosity_get"), out parseSpeed);

                return parseSpeed;
            }
            set { _mf.Option("verbosity", value.ToString(CultureInfo.InvariantCulture)); }
        }

        public string LineSeparator {
            get { return _mf.Option("LineSeparator_get"); }
            set { _mf.Option("LineSeparator", value); }
        }

        public string Version {
            get { return _mf.Option("Version_Get"); }
            set { _mf.Option("Version", value); }
        }

        public string ColumnSeparator {
            get { return _mf.Option("ColumnSeparator_Get"); }
            set { _mf.Option("ColumnSeparator", value); }
        }

        public string TagSeparator {
            get { return _mf.Option("TagSeparator_Get"); }
            set { _mf.Option("TagSeparator", value); }
        }

        public string Quote {
            get { return _mf.Option("Quote_Get"); }
            set { _mf.Option("Quote", value); }
        }

        public string DecimalPoint {
            get { return _mf.Option("decimalpoint_get"); }
            set { _mf.Option("decimalpoint", value); }
        }

        public string ThousandsPoint {
            get { return _mf.Option("thousandspoint_get"); }
            set { _mf.Option("thousandspoint", value); }
        }

        public string StreamMax {
            get { return _mf.Option("streammax_get"); }
            set { _mf.Option("streammax", value); }
        }

        public string Language {
            get { return _mf.Option("Language_Get"); }
            set { _mf.Option("Language", value); }
        }

        public string Inform {
            get { return _mf.Option("inform_get"); }
            set { _mf.Option("inform", value); }
        }

        public InformPreset InformPreset {
            set {
                string strType;
                switch (value) {
                    case InformPreset.HTML:
                    case InformPreset.XML:
                    case InformPreset.PBCore:
                        strType = value.ToString();
                        break;
                    case InformPreset.ReVTMD:
                        strType = "reVTMD";
                        break;
                    case InformPreset.Mpeg7:
                        strType = "MPEG-7";
                        break;
                    default:
                        strType = "";
                        break;
                }

                _mf.Option("inform", strType);
            }
        }

        public string InformReplace {
            get { return _mf.Option("inform_replace_get"); }
            set { _mf.Option("inform_replace", value); }
        }

        public string TraceLevel {
            get { return _mf.Option("trace_level_get"); }
            set { _mf.Option("trace_level", value); }
        }

        public bool TraceTimeSectionOnlyFirstOccurrence {
            get { return _mf.Option("trace_timesection_onlyfirstoccurrence_get") == "1"; }
            set {
                _mf.Option("trace_timesection_onlyfirstoccurrence", value ? "1" : "");
            }
        }

        public TraceFormat TraceFormat {
            get {
                return _mf.Option("trace_format_get") == "CSV"
                    ? TraceFormat.CSV
                    : TraceFormat.Tree;
            }
            set {
                _mf.Option("trace_format", value == TraceFormat.CSV ? "CSV" : "Tree");
            }
        }

        public string DetailsModificator {
            get { return _mf.Option("detailsmodificator_get"); }
            set { _mf.Option("detailsmodificator", value); }
        }

        public string ShowFiles {
            set { _mf.Option("ShowFiles_Set", value); }
        }

        public bool AllowInternetConnection {
            get { return _mf.Option("Internet") == "1"; }
            set {
                _mf.Option("Internet", value ? "1" : "");
            }
        }

        public string CustomMapping {
            set { _mf.Option("custommapping", value); }
        }

        public bool MultipleValues {
            get { return _mf.Option("multiplevalues_get") == "1"; }
            set {
                _mf.Option("multiplevalues", value ? "1" : "");
            }
        }

        public Demux Demux {
            get {
                Demux dm;
                Enum.TryParse(_mf.Option("demux"), true, out dm);
                return dm;
            }
            set { _mf.Option("demux", value.ToString()); }
        }

        /// <summary>Configure or get information about MediaInfoLib</summary>
        /// <param name="option">The option.</param>
        /// <param name="value">The value of option</param>
        /// <returns>Depend of the option: by default "" (nothing) means No, other means Yes</returns>
        public string Custom(string option, string value = "") {
            return _mf.Option(option, value);
        }

    }

}

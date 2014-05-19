
namespace Frost.SharpMediaInfo {

    public class LibraryInfo {

        private readonly MediaFileBase _mi;

        internal LibraryInfo(MediaFileBase mi) {
            _mi = mi;
        }

        public string KnownParameters {
            get { return _mi.Option("info_parameters"); }
        }

        public string KnownParametersCSV(bool complete) {
            return _mi.Option("info_parameters_csv", complete ? "Complete" : "");
        }

        public string KnownCodecs {
            get { return _mi.Option("info_codecs"); }
        }

        public string VersionInfo {
            get { return _mi.Option("info_version"); }
        }

        public string InfoUrl {
            get { return _mi.Option("info_url"); }
        }        
    }
}
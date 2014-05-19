namespace Frost.SharpMediaInfo.Options {
    public class SSL {
        private readonly MediaFileBase _mf;

        internal SSL(MediaFileBase mf) {
            _mf = mf;
        }

        public string CertificateFileName { set { _mf.Option("ssl_certificatefilename", value); } }

        public string CertificateFormat { set { _mf.Option("ssl_certificateFormat", value); } }

        public string PrivateKeyFilename { set { _mf.Option("ssl_privatekeyfilename", value); } }

        public string PrivateKeyFormat { set { _mf.Option("ssl_privatekeyformat", value); } }

        public string CertificateAuthorityFilename { set { _mf.Option("ssl_certificateauthorityfilename", value); } }

        public string CertificateAuthorityPath { set { _mf.Option("ssl_certificateauthoritypath", value); } }

        public string CertificateRevocationListFilename { set { _mf.Option("ssl_certificaterevocationlistfilename", value); } }

        public string IgnoreSecurity { set { _mf.Option("ssl_ignoresecurity", value); } }
    }
}
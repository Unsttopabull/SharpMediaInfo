namespace Frost.SharpMediaInfo.Output.Properties {
    public class ServiceInfo {
        private readonly Media _media;

        public ServiceInfo(Media media) {
            _media = media;
        }

        public string Name { get { return _media["ServiceName"]; } }
        public string Channel { get { return _media["ServiceChannel"]; } }
        public string Url { get { return _media["Service/Url"]; } }
        public string Provider { get { return _media["Service/Provider"]; } }
        public string ProviderrUrl { get { return _media["ServiceProviderr/Url"]; } }
        public string Type { get { return _media["ServiceType"]; } }
    }
}
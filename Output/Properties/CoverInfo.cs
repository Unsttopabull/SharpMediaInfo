
namespace Frost.SharpMediaInfo.Output.Properties {

    public class CoverInfo {
        private Media _media;

        public CoverInfo(Media mediaGeneral) {
            _media = mediaGeneral;
        }

        public string Description { get { return _media["Cover_Description"]; } }
        public string Type { get { return _media["Cover_Type"]; } }
        public string Mime { get { return _media["Cover_Mime"]; } }
        public string Data { get { return _media["Cover_Data"]; } }
    }
}

namespace Frost.SharpMediaInfo.Output.Properties {

    public enum InfoType {
        ScanType,
        ScanOrder,
        PixelAspectRatio,
        DisplayAspectRatio
    }

    public class Info {
        private readonly Media _media;
        private readonly string[] _propNames;

        internal Info(Media mediaVideo, InfoType type) {
            _media = mediaVideo;

            switch (type) {
                case InfoType.ScanType:
                    _propNames = new[] { "ScanType/String", "ScanType_Original", "ScanType_Original/String" };
                    break;
                case InfoType.ScanOrder:
                    _propNames = new[] { "ScanOrder/String", "ScanOrder_Original", "ScanOrder_Original/String" };
                    break;
                case InfoType.PixelAspectRatio:
                    _propNames = new[] { "PixelAspectRatio/String", "PixelAspectRatio_Original", "PixelAspectRatio_Original/String" };
                    break;
                case InfoType.DisplayAspectRatio:
                    _propNames = new[] { "DisplayAspectRatio/String", "DisplayAspectRatio_Original", "DisplayAspectRatio_Original/String" };
                    break;
            }
        }

        public string String { get { return _media[_propNames[0]]; } }

        public float? Original { get { return _media.TryParseFloat(_propNames[1]); } }
        public string OriginalString { get { return _media[_propNames[2]]; } }
    }
}
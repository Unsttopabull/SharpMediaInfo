namespace Frost.SharpMediaInfo.Output.Properties {
    public class PartInfo {
        private readonly Media _media;

        public PartInfo(Media media) {
            _media = media;
        }

        public string Part { get { return _media["Part"]; } }
        public long? PartPosition { get { return _media.TryParseLong("Part/Position"); } }
        public long? PartPositionTotal { get { return _media.TryParseLong("Part/Position_Total"); } }
    }
}
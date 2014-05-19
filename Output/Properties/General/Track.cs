namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class Track {
        private readonly Media _media;

        public Track(Media media) {
            _media = media;
        }

        public string Name { get { return _media["Track"]; } }
        public string More { get { return _media["Track/More"]; } }
        public string Url { get { return _media["Track/Url"]; } }
        public string Sort { get { return _media["Track/Sort"]; } }
        public long? Position { get { return _media.TryParseLong("Track/Position"); } }
        public long? PositionTotal { get { return _media.TryParseLong("Track/Position_Total"); } }
    }
}
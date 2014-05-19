namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class Comic {
        private readonly Media _media;

        public Comic(Media media) {
            _media = media;
        }

        public string Name { get { return _media["Comic"]; } }
        public string More { get { return _media["Comic/More"]; } }
        public long? PositionTotal { get { return _media.TryParseLong("Comic/Position_Total"); } }
    }
}
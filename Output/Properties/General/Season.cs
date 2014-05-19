namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class Season {
        private readonly Media _media;

        public Season(Media media) {
            _media = media;
        }

        public string Name { get { return _media["Season"]; } }
        public long? Position { get { return _media.TryParseLong("Season_Position"); } }
        public long? PositionTotal { get { return _media.TryParseLong("Season_Position_Total"); } }
    }
}
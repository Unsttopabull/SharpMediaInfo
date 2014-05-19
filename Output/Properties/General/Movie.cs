namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class Movie {
        private readonly Media _media;

        public Movie(Media media) {
            _media = media;
        }

        public string Name { get { return _media["Movie"]; } }
        public string More { get { return _media["Movie/More"]; } }
        public string Country { get { return _media["Movie/Country"]; } }
        public string Url { get { return _media["Movie/Url"]; } }
    }
}
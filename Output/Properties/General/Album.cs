namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class Album {
        private readonly MediaGeneral _media;

        public Album(MediaGeneral media) {
            _media = media;
        }

        public string Name { get { return _media["Album"]; } }
        public string More { get { return _media["Album/More"]; } }
        public string Sort { get { return _media["Album/Sort"]; } }
        public string Performer { get { return _media["Album/Performer"]; } }
        public string PerformerSort { get { return _media["Album/Performer/Sort"]; } }
        public string PerformerUrl { get { return _media["Album/Performer/Url"]; } }
    }
}
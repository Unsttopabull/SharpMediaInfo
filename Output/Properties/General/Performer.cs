namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class Performer {
        private readonly Media _media;

        public Performer(Media media) {
            _media = media;
        }

        /// <summary>Main performer/artist of this file</summary>
        public string Name { get { return _media["Performer"]; } }
        
        public string Sort { get { return _media["Performer/Sort"]; } }

        /// <summary>Homepage of the performer/artist</summary>
        public string Url { get { return _media["Performer/Url"]; } }

        /// <summary>Original artist(s)/performer(s).</summary>
        public string OriginalPerformer { get { return _media["Original/Performer"]; } }
    }
}
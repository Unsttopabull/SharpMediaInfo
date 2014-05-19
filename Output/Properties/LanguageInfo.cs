namespace Frost.SharpMediaInfo.Output.Properties {

    public class LanguageInfo {

        private readonly Media _media;

        public LanguageInfo(Media media) {
            _media = media;
        }

        /// <summary>Language name (full)</summary>
        public string Full { get { return _media["Language/String"]; } }

        /// <summary>Language name (full)</summary>
        public string Full1 { get { return _media["Language/String1"]; } }

        /// <summary>Language (2-letter ISO 639-1 if exists, else empty)</summary>
        public string ISO639_Alpha2 { get { return _media["Language/String2"]; } }

        /// <summary>Language (3-letter ISO 639-2 if exists, else empty)</summary>
        public string ISO639_Alpha3 { get { return _media["Language/String3"]; } }

        /// <summary>Language (2-letter ISO 639-1 if exists with optional ISO 3166-1 country separated by a dash if available, e.g. en, en-us, zh-cn, else empty)</summary>
        public string ISO639_Alpha2Variant { get { return _media["Language/String4"]; } }

        /// <summary>More info about Language (e.g. Director's Comment)</summary>
        public string More { get { return _media["Language_More"]; } }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return string.Format("{0}({1} / {2})", Full, ISO639_Alpha2, ISO639_Alpha3);
        }

    }

}

namespace Frost.SharpMediaInfo.Output {

    public class GeneralInfo {

        private readonly Media _media;
        private readonly string[] _propNames;

        internal GeneralInfo(Media media, StreamKind streamKind) {
            _media = media;

            switch (streamKind) {
                case StreamKind.Video:
                    _propNames = new[] { "VideoCount", "Video_Format_List", "Video_Format_WithHint_List", "Video_Codec_List", "Video_Language_List" };
                    break;
                case StreamKind.Audio:
                    _propNames = new[] { "AudioCount", "Audio_Format_List", "Audio_Format_WithHint_List", "Audio_Codec_List", "Audio_Language_List" };
                    break;
                case StreamKind.Text:
                    _propNames = new[] { "TextCount", "Text_Format_List", "Text_Format_WithHint_List", "Text_Codec_List", "Text_Language_List" };
                    break;
                case StreamKind.Other:
                    _propNames = new[] { "OtherCount", "Other_Format_List", "Other_Format_WithHint_List", "Other_Codec_List", "Other_Language_List" };
                    break;
                case StreamKind.Image:
                    _propNames = new[] { "ImageCount", "Image_Format_List", "Image_Format_WithHint_List", "Image_Codec_List", "Image_Language_List" };
                    break;
                case StreamKind.Menu:
                    _propNames = new[] { "MenuCount", "Menu_Format_List", "Menu_Format_WithHint_List", "Menu_Codec_List", "Menu_Language_List" };
                    break;
                default:
                    _propNames = new[] {"", "", "", "", ""};
                    break;
            }
        }

        /// <summary>Number of Stream kind's streams</summary>
        public long? Count { get { return _media.TryParseLong(_propNames[0]); } }

        /// <summary>Stream kind's Codecs in this file, separated by /</summary>
        public string[] FormatList { get { return _media.ParseStringList(_propNames[1]); } }

        /// <summary>Stream kind's Codecs in this file with popular name (hint), separated by /</summary>
        public string[] FormatWithHintList { get { return _media.ParseStringList(_propNames[2]); } }

        /// <summary>Deprecated, do not use in new projects</summary>
        public string[] CodecList { get { return _media.ParseStringList(_propNames[3]); } }

        /// <summary>Stream kind's languages in this file, full names, separated by /</summary>
        public string[] LanguageList { get { return _media.ParseStringList(_propNames[4]); } }

    }

}
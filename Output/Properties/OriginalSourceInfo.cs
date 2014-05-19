namespace Frost.SharpMediaInfo.Output.Properties {
    public class OriginalSourceInfo {
        private readonly Media _media;

        public OriginalSourceInfo(Media media) {
            _media = media;
        }

        /// <summary>Original medium of the material, e.g. vinyl, Audio-CD, Super8 or BetaMax</summary>
        public string Medium { get { return _media["OriginalSourceMedium"]; } }

        /// <summary>Original form of the material, e.g. slide, paper, map</summary>
        public string Form { get { return _media["OriginalSourceForm"]; } }
        /// <summary>Number of colors requested when digitizing, e.g. 256 for images or 32 bit RGB for video</summary>
        public string FormNumColors { get { return _media["OriginalSourceForm/NumColors"]; } }
        /// <summary>Name of the product the file was originally intended for</summary>
        public string FormName { get { return _media["OriginalSourceForm/Name"]; } }
        /// <summary>Describes whether an image has been cropped and, if so, how it was cropped. e.g. 16:9 to 4:3, top and bottom</summary>
        public string FormCropped { get { return _media["OriginalSourceForm/Cropped"]; } }
        /// <summary>Identifies the changes in sharpness for the digitizer requiered to produce the file</summary>
        public string FormSharpness { get { return _media["OriginalSourceForm/Sharpness"]; } }
    }
}
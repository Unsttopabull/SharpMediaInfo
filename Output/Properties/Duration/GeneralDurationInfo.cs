namespace Frost.SharpMediaInfo.Output.Properties.Duration {

    public class GeneralDurationInfo : DurationInfo {

        public GeneralDurationInfo(Media media) : base(media, false) {
        }

        public string Start { get { return MediaStream["Duration_Start"]; } }

        public string End { get { return MediaStream["Duration_End"]; } }

    }

}

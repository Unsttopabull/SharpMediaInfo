#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output.Properties.Duration {

    public class ExtendedDurationInfo : DurationInfo {

        public ExtendedDurationInfo(Media media, bool source) : base(media, source) {
            FirstFrame = new FrameDuration(MediaStream, FrameNumber.FirstFrame, source);
            LastFrame = new FrameDuration(MediaStream, FrameNumber.LastFrame, source);
        }

        public FrameDuration FirstFrame { get; private set; }

        public FrameDuration LastFrame { get; private set; }
    }
}
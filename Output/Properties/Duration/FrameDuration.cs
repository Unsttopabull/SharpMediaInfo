
using System;

namespace Frost.SharpMediaInfo.Output.Properties.Duration {

    internal enum FrameNumber {
        FirstFrame,
        LastFrame
    }

    public class FrameDuration {

        private readonly Media _media;
        private readonly string[] _propNames;

        internal FrameDuration(Media media, FrameNumber frame, bool source) {
            _media = media;

            switch (frame) {
                case FrameNumber.FirstFrame:
                    if (source) {
                        _propNames = new[] { "Source_Duration_FirstFrame", "Source_Duration_FirstFrame/String", "Source_Duration_FirstFrame/String1", "Source_Duration_FirstFrame/String2", "Source_Duration_FirstFrame/String3" };
                    }
                    _propNames = new[] { "Duration_FirstFrame", "Duration_FirstFrame/String", "Duration_FirstFrame/String1", "Duration_FirstFrame/String2", "Duration_FirstFrame/String3" };
                    break;
                case FrameNumber.LastFrame:
                    if (source) {
                        _propNames = new[] { "Source_Duration_LastFrame", "Source_Duration_LastFrame/String", "Source_Duration_LastFrame/String1", "Source_Duration_LastFrame/String2", "Source_Duration_LastFrame/String3" };
                    }
                    _propNames = new[] { "Duration_LastFrame", "Duration_LastFrame/String", "Duration_LastFrame/String1", "Duration_LastFrame/String2", "Duration_LastFrame/String3" };
                    break;
            }
        }

        /// <summary>Duration of the frame if it is longer than others, in ms</summary>
        public TimeSpan? Duration { get { return _media.TryParseTimeSpan(_propNames[0]); } }

        /// <summary>Duration of the frame if it is longer than others, in format : XXx YYy only, YYy omited if zero</summary>
        public string String { get { return _media[_propNames[1]]; } }

        /// <summary>Duration of the frame if it is longer than others, in format : HHh MMmn SSs MMMms, XX omited if zero</summary>
        public string String1 { get { return _media[_propNames[2]]; } }

        /// <summary>Duration of the frame if it is longer than others, in format : XXx YYy only, YYy omited if zero</summary>
        public string String2 { get { return _media[_propNames[3]]; } }

        /// <summary>Duration of the frame if it is longer than others, in format : HH:MM:SS.MMM</summary>
        public string String3 { get { return _media[_propNames[4]]; } }

    }

}
using System;
using Frost.SharpMediaInfo.Output.Properties;
using Frost.SharpMediaInfo.Output.Properties.Duration;
using Frost.SharpMediaInfo.Output.Properties.Formats;

#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output {
    public class MediaOther : EnumerableMedia<MediaOther> {

        public MediaOther(MediaFileBase mediaInfo) : base(mediaInfo, StreamKind.Other) {
            FormatInfo = new Format(this);
            DurationInfo = new GeneralDurationInfo(this);
            LanguageInfo = new LanguageInfo(this);
        }

        /// <summary>Info about the Format used</summary>
        public Format FormatInfo { get; private set; }

        public string Type { get { return this["Type"]; } }

        /// <summary>How this file is muxed in the container</summary>
        public string MuxingMode { get { return this["MuxingMode"]; } }

        /// <summary>Play time of the stream in ms</summary>
        public TimeSpan? Duration { get { return TryParseTimeSpan("Duration"); } }
        public GeneralDurationInfo DurationInfo { get; private set; }

        /// <summary>Frames per second</summary>
        public float? FrameRate { get { return TryParseFloat("FrameRate"); } }
        /// <summary>Frames per second (with measurement)</summary>
        public string FrameRateString { get { return this["FrameRate/String"]; } }

        /// <summary>Number of frames</summary>
        public long? FrameCount { get { return TryParseLong("FrameCount"); } }

        /// <summary>TimeStamp fixed in the stream (relative) IN MS</summary>
        public long? TimeStampFirstFrame { get { return TryParseLong("TimeStamp_FirstFrame"); } }
        /// <summary>TimeStamp with measurement</summary>
        public string TimeStampFirstFrameString { get { return this["TimeStamp_FirstFrame/String"]; } }
        /// <summary>TimeStamp with measurement</summary>
        public string TimeStampFirstFrameString1 { get { return this["TimeStamp_FirstFrame/String1"]; } }
        /// <summary>TimeStamp with measurement</summary>
        public string TimeStampFirstFrameString2 { get { return this["TimeStamp_FirstFrame/String2"]; } }
        /// <summary>TimeStamp in format : HH:MM:SS.MMM</summary>
        public string TimeStampFirstFrameString3 { get { return this["TimeStamp_FirstFrame/String3"]; } }

        /// <summary>Time code in HH:MM:SS:FF (HH:MM:SS</summary>
        public string TimeCodeFirstFrame { get { return this["TimeCode_FirstFrame"]; } }
        /// <summary>Time code settings</summary>
        public string TimeCodeSettings { get { return this["TimeCode_Settings"]; } }

        /// <summary>Name of the track</summary>
        public string Title { get { return this["Title"]; } }

        /// <summary>Language (2-letter ISO 639-1 if exists, else 3-letter ISO 639-2, and with optional ISO 3166-1 country separated by a dash if available, e.g. en, en-us, zh-cn)</summary>
        public string Language { get { return this["Language"]; } }
        public LanguageInfo LanguageInfo { get; private set; }
    }
}
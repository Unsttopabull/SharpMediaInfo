using System;
using Frost.SharpMediaInfo.Output.Properties;
using Frost.SharpMediaInfo.Output.Properties.Codecs;
using Frost.SharpMediaInfo.Output.Properties.Delay;
using Frost.SharpMediaInfo.Output.Properties.Duration;
using Frost.SharpMediaInfo.Output.Properties.Formats;

#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output {
    public class MediaMenu : EnumerableMedia<MediaMenu> {

        public MediaMenu(MediaFileBase mediaInfo) : base(mediaInfo, StreamKind.Menu) {
            CodecInfo = new Codec(this);
            FormatInfo = new Format(this);
            DurationInfo = new GeneralDurationInfo(this);
            LanguageInfo = new LanguageInfo(this);
            ServiceInfo = new ServiceInfo(this);
            DelayInfo = new DelayInfo(this, false);
        }

        public Codec CodecInfo { get; private set; }
        /// <summary>Info about the Format used</summary>
        public Format FormatInfo { get; private set; }

        /// <summary>Play time of the stream in ms</summary>
        public TimeSpan? Duration { get { return TryParseTimeSpan("Duration"); } }
        public GeneralDurationInfo DurationInfo { get; private set; }

        /// <summary>Delay fixed in the stream (relative) IN MS</summary>
        public TimeSpan? Delay { get { return TryParseTimeSpan("Delay"); } }
        public DelayInfo DelayInfo { get; private set; }

        /// <summary>List of programs available</summary>
        public string List { get { return this["List"]; } }
        /// <summary>List of programs available</summary>
        public string ListStreamKind { get { return this["List_StreamKind"]; } }
        /// <summary>List of programs available</summary>
        public string ListStreamPos { get { return this["List_StreamPos"]; } }
        /// <summary>List of programs available</summary>
        public string ListString { get { return this["List/String"]; } }

        /// <summary>Name of this menu</summary>
        public string Title { get { return this["Title"]; } }

        public string Language { get { return this["Language"]; } }
        public LanguageInfo LanguageInfo { get; private set; }

        public ServiceInfo ServiceInfo { get; private set; }

        public string NetworkName { get { return this["NetworkName"]; } }
        public string OriginalNetworkName { get { return this["Original/NetworkName"]; } }

        public string Countries { get { return this["Countries"]; } }

        public string TimeZones { get { return this["TimeZones"]; } }

        /// <summary>Used by third-party developers to know about the beginning of the chapters list, to be used by Get(Stream_Menu, x, Pos), where Pos is an Integer between Chapters_Pos_Begin and Chapters_Pos_End</summary>
        public long? ChaptersBeginPosition { get { return TryParseLong("Chapters_Pos_Begin"); } }

        /// <summary>Used by third-party developers to know about the end of the chapters list (this position excluded)</summary>
        public long? ChaptersEndPosition { get { return TryParseLong("Chapters_Pos_End"); } }
    }
}
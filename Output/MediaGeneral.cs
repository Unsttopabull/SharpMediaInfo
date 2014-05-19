using System;
using Frost.SharpMediaInfo.Output.Properties;
using Frost.SharpMediaInfo.Output.Properties.BitRate;
using Frost.SharpMediaInfo.Output.Properties.Codecs;
using Frost.SharpMediaInfo.Output.Properties.Delay;
using Frost.SharpMediaInfo.Output.Properties.Duration;
using Frost.SharpMediaInfo.Output.Properties.Formats;
using Frost.SharpMediaInfo.Output.Properties.General;

#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output {
    public class MediaGeneral : Media {

        internal MediaGeneral(MediaFileBase mediaInfo) : base(mediaInfo, StreamKind.General) {
            CachedStreamCount = 1;
            FormatInfo = new GeneralFormat(this);
            Codec = new GeneralCodec(this);
            DurationInfo = new GeneralDurationInfo(this);
            DelayInfo = new GeneralDelayInfo(this);
            StreamSizeInfo = new StreamSizeInfo(this);
            OverallBitRateInfo = new OverallBitRateInfo(this);
            Season = new Season(this);
            Movie = new Movie(this);
            Album = new Album(this);
            Comic = new Comic(this);
            Track = new Track(this);
            Performer = new Performer(this);
            EncodingLibraryInfo = new EncodingLibraryInfo(this);
            Service = new ServiceInfo(this);
            Part = new PartInfo(this);
            CoverInfo = new CoverInfo(this);
            OriginalSourceInfo = new OriginalSourceInfo(this);
            FileInfo = new MediaFileInfo(this);

            Video = new GeneralInfo(this, StreamKind.Video);
            Audio = new GeneralInfo(this, StreamKind.Audio);
            Text = new GeneralInfo(this, StreamKind.Text);
            Other = new GeneralInfo(this, StreamKind.Other);
            Image = new GeneralInfo(this, StreamKind.Image);
            Menu = new GeneralInfo(this, StreamKind.Menu);
        }

        /// <summary>Number of general streams</summary>
        public long? GeneralCount { get { return TryParseLong("GeneralCount"); } }

        public GeneralInfo Video { get; private set; }
        public GeneralInfo Audio { get; private set; }
        public GeneralInfo Text { get; private set; }
        public GeneralInfo Other { get; private set; }
        public GeneralInfo Image { get; private set; }
        public GeneralInfo Menu { get; private set; }

        /// <summary>Format used</summary>
        public GeneralFormat FormatInfo { get; private set; }

        /// <summary>Internet Media Type (aka MIME Type, Content-Type)</summary>
        public string MIME { get { return this["InternetMediaType"]; } }

        /// <summary>If Audio and video are muxed</summary>
        public string Interleaved { get { return this["Interleaved"]; } }

        /// <summary>Deprecated, do not use in new projects</summary>
        public GeneralCodec Codec { get; private set; }

        /// <summary>Play time of the stream in ms</summary>
        public TimeSpan? Duration { get { return TryParseTimeSpan("Duration"); } }
        public GeneralDurationInfo DurationInfo { get; private set; }

        /// <summary>Bit rate of all streams in bps</summary>
        public float? OverallBitRate { get { return TryParseFloat("OverallBitRate"); } }
        public OverallBitRateInfo OverallBitRateInfo { get; private set; }

        /// <summary>Delay fixed in the stream (relative) IN MS</summary>
        public long? Delay { get { return TryParseLong("Delay"); } }
        public GeneralDelayInfo DelayInfo { get; private set; }

        /// <summary>Stream size in bytes</summary>
        public long? StreamSize { get { return TryParseLong("StreamSize"); } }
        public StreamSizeInfo StreamSizeInfo { get; private set; }

        public long? HeaderSize { get { return TryParseLong("HeaderSize"); } }
        public long? DataSize { get { return TryParseLong("DataSize"); } }
        public long? FooterSize { get { return TryParseLong("FooterSize"); } }

        public string IsStreamable { get { return this["IsStreamable"]; } }

        /// <summary>The gain to apply to reach 89dB SPL on playback</summary>
        public string AlbumReplayGainGain { get { return this["Album_ReplayGain_Gain"]; } }
        /// <summary>The maximum absolute peak value of the item</summary>
        public string AlbumReplayGainPeak { get { return this["Album_ReplayGain_Peak"]; } }

        public string Encryption { get { return this["Encryption"]; } }

        /// <summary>(Generic)Title of file</summary>
        public string Title { get { return this["Title"]; } }
        /// <summary>(Generic)More info about the title of file</summary>
        public string TitleMore { get { return this["Title/More"]; } }
        /// <summary>(Generic)Url</summary>
        public string TitleUrl { get { return this["Title/Url"]; } }

        /// <summary>Univers movies belong to, e.g. Starwars, Stargate, Buffy, Dragonballs</summary>
        public string Domain { get { return this["Domain"]; } }
        /// <summary>Name of the series, e.g. Starwars movies, Stargate SG-1, Stargate Atlantis, Buffy, Angel</summary>
        public string Collection { get { return this["Collection"]; } }

        public Season Season { get; private set; }
        public Movie Movie { get; private set; }
        public Album Album { get; private set; }
        public Comic Comic { get; private set; }
        public PartInfo Part { get; private set; }
        public Track Track { get; private set; }

        public string Grouping { get { return this["Grouping"]; } }
        public string Chapter { get { return this["Chapter"]; } }
        public string SubTrack { get { return this["SubTrack"]; } }

        public string OriginalAlbum { get { return this["Original/Album"]; } }
        public string OriginalMovie { get { return this["Original/Movie"]; } }
        public string OriginalPart { get { return this["Original/Part"]; } }
        public string OriginalTrack { get { return this["Original/Track"]; } }

        public string Compilation { get { return this["Compilation"]; } }
        public string CompilationString { get { return this["Compilation/String"]; } }

        public Performer Performer { get; private set; }

        public string Accompaniment { get { return this["Accompaniment"]; } }

        public string Composer { get { return this["Composer"]; } }
        public string ComposerNationality { get { return this["Composer/Nationality"]; } }

        public string Arranger { get { return this["Arranger"]; } }
        public string Lyricist { get { return this["Lyricist"]; } }
        public string OriginalLyricist { get { return this["Original/Lyricist"]; } }
        public string Conductor { get { return this["Conductor"]; } }

        public string Director { get { return this["Director"]; } }
        public string AssistantDirector { get { return this["AssistantDirector"]; } }
        public string DirectorOfPhotography { get { return this["DirectorOfPhotography"]; } }

        public string SoundEngineer { get { return this["SoundEngineer"]; } }
        public string ArtDirector { get { return this["ArtDirector"]; } }
        public string ProductionDesigner { get { return this["ProductionDesigner"]; } }
        public string Choregrapher { get { return this["Choregrapher"]; } }
        public string CostumeDesigner { get { return this["CostumeDesigner"]; } }

        public string Actor { get { return this["Actor"]; } }
        public string ActorCharacter { get { return this["Actor_Character"]; } }

        public string WrittenBy { get { return this["WrittenBy"]; } }
        public string ScreenplayBy { get { return this["ScreenplayBy"]; } }
        public string EditedBy { get { return this["EditedBy"]; } }
        public string CommissionedBy { get { return this["CommissionedBy"]; } }

        public string Producer { get { return this["Producer"]; } }
        public string CoProducer { get { return this["CoProducer"]; } }
        public string ExecutiveProducer { get { return this["ExecutiveProducer"]; } }

        public string MusicBy { get { return this["MusicBy"]; } }
        public string DistributedBy { get { return this["DistributedBy"]; } }
        public string OriginalSourceFormDistributedBy { get { return this["OriginalSourceForm/DistributedBy"]; } }
        public string MasteredBy { get { return this["MasteredBy"]; } }
        public string EncodedBy { get { return this["EncodedBy"]; } }
        public string RemixedBy { get { return this["RemixedBy"]; } }
        public string ProductionStudio { get { return this["ProductionStudio"]; } }
        public string ThanksTo { get { return this["ThanksTo"]; } }

        public string Publisher { get { return this["Publisher"]; } }
        public string PublisherURL { get { return this["Publisher/URL"]; } }

        public string Label { get { return this["Label"]; } }
        public string Genre { get { return this["Genre"]; } }
        public string Mood { get { return this["Mood"]; } }
        public string ContentType { get { return this["ContentType"]; } }
        public string Subject { get { return this["Subject"]; } }
        public string Description { get { return this["Description"]; } }
        public string Keywords { get { return this["Keywords"]; } }
        public string Summary { get { return this["Summary"]; } }
        public string Synopsis { get { return this["Synopsis"]; } }
        public string Period { get { return this["Period"]; } }

        public string LawRating { get { return this["LawRating"]; } }
        public string LawRatingReason { get { return this["LawRating_Reason"]; } }
        public string Icra { get { return this["ICRA"]; } }

        public string ReleasedDate { get { return this["Released_Date"]; } }
        public string OriginalReleasedDate { get { return this["Original/Released_Date"]; } }

        public string RecordedDate { get { return this["Recorded_Date"]; } }
        public string EncodedDate { get { return this["Encoded_Date"]; } }
        public string TaggedDate { get { return this["Tagged_Date"]; } }
        public string WrittenDate { get { return this["Written_Date"]; } }
        public string MasteredDate { get { return this["Mastered_Date"]; } }

        public MediaFileInfo FileInfo { get; private set; }

        public string RecordedLocation { get { return this["Recorded_Location"]; } }
        public string WrittenLocation { get { return this["Written_Location"]; } }
        public string ArchivalLocation { get { return this["Archival_Location"]; } }

        public string EncodedApplication { get { return this["Encoded_Application"]; } }
        public string EncodedApplicationUrl { get { return this["Encoded_Application/Url"]; } }

        /// <summary>Software used to create the file</summary>
        public string EncodedLibrary { get { return this["Encoded_Library"]; } }
        public EncodingLibraryInfo EncodingLibraryInfo { get; private set; }

        public string Cropped { get { return this["Cropped"]; } }
        public string Dimensions { get { return this["Dimensions"]; } }
        public string DotsPerInch { get { return this["DotsPerInch"]; } }
        public string Lightness { get { return this["Lightness"]; } }

        public OriginalSourceInfo OriginalSourceInfo { get; private set; }

        public string TaggedApplication { get { return this["Tagged_Application"]; } }

        public string BPM { get { return this["BPM"]; } }
        public string ISRC { get { return this["ISRC"]; } }
        public string ISBN { get { return this["ISBN"]; } }

        public string BarCode { get { return this["BarCode"]; } }
        public string LCCN { get { return this["LCCN"]; } }

        public string CatalogNumber { get { return this["CatalogNumber"]; } }
        public string LabelCode { get { return this["LabelCode"]; } }
        public string Owner { get { return this["Owner"]; } }

        public string Copyright { get { return this["Copyright"]; } }
        public string CopyrightUrl { get { return this["Copyright/Url"]; } }
        public string ProducerCopyright { get { return this["Producer_Copyright"]; } }

        public string TermsOfUse { get { return this["TermsOfUse"]; } }

        public ServiceInfo Service { get; private set; }

        public string NetworkName { get { return this["NetworkName"]; } }
        public string OriginalNetworkName { get { return this["OriginalNetworkName"]; } }

        public string Country { get { return this["Country"]; } }
        public string TimeZone { get { return this["TimeZone"]; } }

        public string Cover { get { return this["Cover"]; } }
        public CoverInfo CoverInfo { get; private set; }

        public string Lyrics { get { return this["Lyrics"]; } }
        public string Comment { get { return this["Comment"]; } }
        public string Rating { get { return this["Rating"]; } }

        public string AddedDate { get { return this["Added_Date"]; } }

        public string PlayedFirstDate { get { return this["Played_First_Date"]; } }
        public string PlayedLastDate { get { return this["Played_Last_Date"]; } }
        public long? PlayedCount { get { return TryParseLong("Played_Count"); } }

        public long? EPGPositionsBegin { get { return TryParseLong("EPG_Positions_Begin"); } }
        public long? EPGPositionsEnd { get { return TryParseLong("EPG_Positions_End"); } }
    }
}
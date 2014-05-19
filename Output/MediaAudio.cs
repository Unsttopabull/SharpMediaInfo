using System;
using Frost.SharpMediaInfo.Output.Properties;
using Frost.SharpMediaInfo.Output.Properties.BitRate;
using Frost.SharpMediaInfo.Output.Properties.Codecs;
using Frost.SharpMediaInfo.Output.Properties.Delay;
using Frost.SharpMediaInfo.Output.Properties.Duration;
using Frost.SharpMediaInfo.Output.Properties.Formats;

#pragma warning disable 1591

namespace Frost.SharpMediaInfo.Output {

    // ReSharper disable UnusedMember.Global
    public class MediaAudio : EnumerableMedia<MediaAudio> {

        internal MediaAudio(MediaFileBase mediaInfo) : base(mediaInfo, StreamKind.Audio) {
            Format = new AudioFormat(this);
            CodecInfo = new AudioCodec(this);
            BitRateInfo = new BitRateInfo(this);
            DurationInfo = new ExtendedDurationInfo(this, false);
            SourceDurationInfo = new ExtendedDurationInfo(this, true);
            LanguageInfo = new LanguageInfo(this);
            DelayInfo = new DelayInfo(this, false);
            DelayOriginalInfo = new DelayInfo(this, true);
            EncodingLibraryInfo = new EncodingLibraryInfo(this);
            VideoDelayInfo = new VideoDelayInfo(this);
            Video0DelayInfo = new VideoDelayInfo(this);
            Interleave = new Interleave(this);
            ChannelInfo = new ChannelInfo(this);

            StreamSizeInfo = new StreamSizeInfo(this);
            SourceStreamSizeInfo = new StreamSizeInfo(this, StreamSizeType.SourceStreamSize);
            StreamSizeEncodedInfo = new StreamSizeInfo(this, StreamSizeType.EncodedStreamSize);
            SourceStreamSizeEncodedInfo = new StreamSizeInfo(this, StreamSizeType.EncodedSourceStreamSize);
        }

        /// <summary>Format used</summary>
        public AudioFormat Format { get; private set; }

        /// <summary>Codec used</summary>
        public AudioCodec CodecInfo { get; private set; }

        /// <summary>Internet Media Type (aka MIME Type, Content-Type)</summary>
        public string MIME { get { return this["InternetMediaType"]; } }

        /// <summary>How this stream is muxed in the container</summary>
        public string MuxingMode { get { return this["MuxingMode"]; } }
        /// <summary>More info (text) about the muxing mode</summary>
        public string MuxingModeMoreInfo { get { return this["MuxingMode_MoreInfo"]; } }

        /// <summary>Play time of the stream in ms</summary>
        public TimeSpan? Duration { get { return TryParseTimeSpan("Duration"); } }
        public ExtendedDurationInfo DurationInfo { get; private set; }

        /// <summary>Source Play time of the stream</summary>
        public TimeSpan? SourceDuration { get { return TryParseTimeSpan("Source_Duration"); } }
        public ExtendedDurationInfo SourceDurationInfo { get; private set; }

        /// <summary>Bit rate in bps</summary>
        public float? BitRate { get { return TryParseFloat("BitRate"); } }
        public BitRateInfo BitRateInfo { get; private set; }

        /// <summary>Number of channels</summary>
        public long? NumberOfChannels { get { return TryParseLong("Channel(s)"); } }
        public ChannelInfo ChannelInfo { get; private set; }

        /// <summary>Sampling rate in Hz</summary>
        public long? SamplingRate { get { return TryParseLong("SamplingRate"); } }
        /// <summary>Sampling rate in KHz</summary>
        public string SamplingRateString { get { return this["SamplingRate/String"]; } }

        /// <summary>Sample count (based on sampling rate)</summary>
        public long? SamplesCount { get { return TryParseLong("SamplingCount"); } }
        /// <summary>Source Sample count (based on sampling rate)</summary>
        public long? SourceSamplesCount { get { return TryParseLong("Source_SamplingCount"); } }

        /// <summary>Frames per second</summary>
        public float? FrameRate { get { return TryParseFloat("FrameRate"); } }
        /// <summary>Frames per second (with measurement)</summary>
        public string FrameRateString { get { return this["FrameRate/String"]; } }

        /// <summary>Frame count (a frame contains a count of samples depends of the format)</summary>
        public long? FrameCount { get { return TryParseLong("FrameCount"); } }
        /// <summary>Source Frame count (a frame contains a count of samples depends of the format)</summary>
        public long? SourceFrameCount { get { return TryParseLong("Source_FrameCount"); } }

        public string Resolution { get { return this["Resolution"]; } }
        public string ResolutionString { get { return this["Resolution/String"]; } }

        /// <summary>Resolution in bits (8, 16, 20, 24)</summary>
        public long? BitDepth { get { return TryParseLong("BitDepth"); } }
        /// <summary>Resolution in bits (8, 16, 20, 24)</summary>
        public string BitDepthString { get { return this["BitDepth/String"]; } }

        /// <summary>Compression mode (Lossy or Lossless)</summary>
        public CompressionMode CompressionMode { get { return ParseCompressionMode("Compression_Mode"); } }
        /// <summary>Compression mode (Lossy or Lossless)</summary>
        public string CompressionModeString { get { return this["Compression_Mode/String"]; } }
        /// <summary>Current stream size divided by uncompressed stream size</summary>
        public string CompressionRatio { get { return this["Compression_Ratio"]; } }

        /// <summary>Delay fixed in the stream (relative) IN MS</summary>
        public TimeSpan? Delay { get { return TryParseTimeSpan("Delay"); } }
        public DelayInfo DelayInfo { get; private set; }

        /// <summary>Delay fixed in the raw stream (relative) IN MS</summary>
        public TimeSpan? DelayOriginal { get { return TryParseTimeSpan("Delay_Original"); } }

        public DelayInfo DelayOriginalInfo { get; private set; }

        /// <summary>Delay fixed in the stream (absolute / video)</summary>
        public long? VideoDelay { get { return TryParseLong("Video_Delay"); } }
        public VideoDelayInfo VideoDelayInfo { get; private set; }

        public long? Video0Delay { get { return TryParseLong("Video0_Delay"); } }
        public VideoDelayInfo Video0DelayInfo { get; private set; }

        /// <summary>The gain to apply to reach 89dB SPL on playback</summary>
        public string ReplayGainGain { get { return this["ReplayGain_Gain"]; } }
        public string ReplayGainGainString { get { return this["ReplayGain_Gain/String"]; } }
        /// <summary>The maximum absolute peak value of the item</summary>
        public string ReplayGainPeak { get { return this["ReplayGain_Peak"]; } }

        /// <summary>Streamsize in bytes</summary>
        public long? StreamSize { get { return TryParseLong("StreamSize"); } }
        public StreamSizeInfo StreamSizeInfo { get; private set; }

        /// <summary>Source Streamsize in bytes</summary>
        public long? SourceStreamSize { get { return TryParseLong("Source_StreamSize"); } }
        public StreamSizeInfo SourceStreamSizeInfo { get; private set; }

        /// <summary>Encoded Streamsize in bytes</summary>
        public long? StreamSizeEncoded { get { return TryParseLong("StreamSize_Encoded"); } }
        public StreamSizeInfo StreamSizeEncodedInfo { get; private set; }

        /// <summary>Source Encoded Streamsize in bytes</summary>
        public long? SourceStreamSizeEncoded { get { return TryParseLong("Source_StreamSize_Encoded"); } }
        public StreamSizeInfo SourceStreamSizeEncodedInfo { get; private set; }

        /// <summary>How this stream file is aligned in the container</summary>
        public AudioAlignment Alignment { 
            get {
                switch (this["Alignment"]) {
                    case "Split":
                        return AudioAlignment.Split;
                    case "Aligned":
                        return AudioAlignment.Aligned;
                    default:
                        return AudioAlignment.Unknown;
                }
            }
        }
        /// <summary>Where this stream file is aligned in the container</summary>
        public string AlignmentString { get { return this["Alignment/String"]; } }

        public Interleave Interleave { get; private set; }

        /// <summary>Name of the track</summary>
        public string Title { get { return this["Title"]; } }

        /// <summary>Software used to create the file</summary>
        public string EncodingLibrary { get { return this["Encoded_Library"]; } }
        public EncodingLibraryInfo EncodingLibraryInfo { get; private set; }

        /// <summary>Language (2-letter ISO 639-1 if exists, else 3-letter ISO 639-2, and with optional ISO 3166-1 country separated by a dash if available, e.g. en, en-us, zh-cn)</summary>
        public string Language { get { return this["Language"]; } }
        public LanguageInfo LanguageInfo { get; private set; }

        /// <summary>Set if that track should be used if no language found matches the user preference.</summary>
        public string Default { get { return this["Default"]; } }
        /// <summary>Set if that track should be used if no language found matches the user preference.</summary>
        public string DefaultString { get { return this["DefaultString"]; } }

        /// <summary>Set if that track should be used if no language found matches the user preference.</summary>
        public string Forced { get { return this["Forced"]; } }
        /// <summary>Set if that track should be used if no language found matches the user preference.</summary>
        public string ForcedString { get { return this["ForcedString"]; } }

        /// <summary>UTC time that the encoding of this item was completed began.</summary>
        public string EncodedDate { get { return this["Encoded_Date"]; } }

        /// <summary>UTC time that the tags were done for this item.</summary>
        public string TaggedDate { get { return this["Tagged_Date"]; } }

        public string Encryption { get { return this["Encryption"]; } }
    }
    // ReSharper restore UnusedMember.Global
}

namespace Frost.SharpMediaInfo.Output.Properties.Duration {

    public class DurationInfo {

        protected readonly Media MediaStream;
        private readonly bool _originalDuration;

        protected DurationInfo(Media media, bool originalDuration) {
            MediaStream = media;
            _originalDuration = originalDuration;
        }
        
        /// <summary>Play time in format : XXx YYy only, YYy omited if zero</summary>
        /// <example>\eg{ <c>"22mn 17s"</c> or <c>"1h 41mn"</c>}</example>
        public string String { get { return _originalDuration ? MediaStream["Source_Duration/String"] : MediaStream["Duration/String"]; } }

        /// <summary>Play time in format : HHh MMmn SSs MMMms, XX omited if zero</summary>
        /// <example>\eg{ <c>"22mn 17s 336ms"</c> }</example>
        public string String1 { get { return _originalDuration ? MediaStream["Source_Duration/String1"] : MediaStream["Duration/String1"]; } }

        /// <summary>Play time in format : XXx YYy only, YYy omited if zero</summary>
        /// <example>\eg{ <c>"22mn 17s"</c> }</example>
        public string String2 { get { return _originalDuration ? MediaStream["Source_Duration/String2"] : MediaStream["Duration/String2"]; } }

        /// <summary>Play time in format : HH:MM:SS.MMM</summary>
        /// <example>\eg{ <c>"00:22:17.336"</c> }</example>
        public string String3 { get { return _originalDuration ? MediaStream["Source_Duration/String3"] : MediaStream["Duration/String3"]; } }
    }
}
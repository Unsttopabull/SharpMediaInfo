namespace Frost.SharpMediaInfo.Output.Properties.Delay {
    public class GeneralDelayInfo {
        protected readonly Media MediaStream;
        protected readonly bool Original;

        public GeneralDelayInfo(Media media) : this(media, false){
        }

        internal GeneralDelayInfo(Media media, bool original) {
            MediaStream = media;
            Original = original;
        }

        /// <summary>Delay in format : XXx YYy only, YYy omited if zero.</summary>
        /// <example>\eg{ <c>"22mn 17s"</c> or <c>"1h 41mn"</c>}</example>
        public string String { get { return Original ? MediaStream["Delay_Original/String"] : MediaStream["Delay/String"]; } }

        /// <summary>Delay in format : HHh MMmn SSs MMMms, XX omited if zero.</summary>
        /// <example>\eg{ <c>"22mn 17s 336ms"</c> }</example>
        public string String1 { get { return Original ? MediaStream["Delay_Original/String1"] : MediaStream["Delay/String1"]; } }

        /// <summary>Delay in format: XXx YYy only, YYy omited if zero.</summary>
        /// <example>\eg{ <c>"22mn 17s"</c> }</example>
        public string String2 { get { return Original ? MediaStream["Delay_Original/String2"] : MediaStream["Delay/String2"]; } }

        /// <summary>Delay in format: HH:MM:SS.MMM</summary>
        /// <example>\eg{ <c>"00:22:17.336"</c> }</example>
        public string String3 { get { return Original ? MediaStream["Delay_Original/String3"] : MediaStream["Delay/String3"]; } }         
    }
}
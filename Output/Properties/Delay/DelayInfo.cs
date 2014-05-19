
namespace Frost.SharpMediaInfo.Output.Properties.Delay {

    public class DelayInfo : GeneralDelayInfo {

        internal DelayInfo(Media media, bool original) : base(media, original) {
        }

        /// <summary>Delay in format : HH:MM:SS:FF (HH:MM:SS</summary>
        public string String4 { get { return Original ? MediaStream["Delay_Original/String4"] : MediaStream["Delay/String4"]; } }


        /// <summary>Delay settings (in case of timecode for example)</summary>
        public string[] Settings { get { return Original ? MediaStream.ParseStringList("Delay_Original_Settings") : MediaStream.ParseStringList("Delay_Settings"); } }

        /// <summary>Delay drop frame</summary>
        public bool? DropFrame { get { return Original ? MediaStream.TryParseBool("Delay_Original_DropFrame") : MediaStream.TryParseBool("Delay_DropFrame"); } }

        /// <summary>Delay source (Container or Stream or empty)</summary>
        public long? Source { get { return Original ? MediaStream.TryParseLong("Delay_Original_Source") : MediaStream.TryParseLong("Delay_Source"); } }
        /// <summary>Delay source (Container or Stream or empty)</summary>
        public string SourceString { get { return Original ? MediaStream["Delay_Original_Source/String"] : MediaStream["Delay_Source/String"]; } }
    }
}

namespace Frost.SharpMediaInfo.Output.Properties.BitRate {

    public class BitRateInfo : OverallBitRateInfo {

        public BitRateInfo(Media media) : base(media, false) {
        }

        /// <summary>Encoded (with forced padding) bit rate in bps, if some container padding is present</summary>
        public float? Encoded { get { return MediaStream.TryParseFloat("BitRate_Encoded"); } }

        /// <summary>Encoded (with forced padding) bit rate (with measurement), if some container padding is present</summary>
        public string EncodedString { get { return MediaStream["BitRate_Encoded/String"]; } }
    }
}
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.GeoLocation
{
    public partial class Location
    {
        public const double MaxLongitude = 180;
        public const double MinLongitude = -180;
        public const double MaxLatitude = 90;
        public const double MinLatitude = -90;

        public static readonly double AbsoluteLatitdueRange = Math.Abs(MinLatitude) + MaxLatitude;
        public static readonly double AbsoluteLongitudeRange = Math.Abs(MinLongitude) + MaxLongitude;


        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
            => $"Lat: {Latitude},  Long: {Longitude}";

        public string PrettyString()
        {
            return $"{PrettyLatitude()}, {PrettyLongitude()}";
        }

        public string PrettyLatitude()
        {
            var latMod = Latitude < 0 ? "S" : "N";
            return $"{Math.Abs( Latitude )}°{latMod}";
        }

        public string PrettyLongitude()
        {
            var longMod = Longitude < 0 ? "W" : "E";
            return $"{Math.Abs( Longitude )}°{longMod}";
        }

        public static Location GetRandom( Random rng )
        {
            var lat = (rng.NextDouble() * AbsoluteLatitdueRange) + MinLatitude;
            var lng = (rng.NextDouble() * AbsoluteLongitudeRange) + MinLongitude;
            return new() { Latitude = lat, Longitude = lng };
        }
    }
}

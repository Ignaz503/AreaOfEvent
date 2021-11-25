using System.Collections.Generic;

namespace AreaOfEvent.Shared.GeoLocation
{
    public partial class Location
    {
        public class Special
        {
            public static readonly NamedLocation NorthPole = new NamedLocation {Name = nameof(NorthPole), Latitude = 90.0, Longitude = -135.0d };
            public static readonly NamedLocation SouthPole = new NamedLocation {Name = nameof(SouthPole), Longitude = 45.0d, Latitude = -90.0d};
            public static readonly NamedLocation Null = new NamedLocation{Name = nameof(Null), Latitude = 0d, Longitude = 0d };
            public static NamedLocation ZeroLatitudeMaxPositiveLongitude = new NamedLocation{Name = nameof(ZeroLatitudeMaxPositiveLongitude), Latitude = 0, Longitude = 180d};
            public static readonly NamedLocation ZeroLatitudeMaxNegativeLongitude = new NamedLocation{Name = nameof(ZeroLatitudeMaxNegativeLongitude), Latitude = 0, Longitude = -180d};
            public static readonly NamedLocation ZeroLatitudeHalfMaxPositiveLongitude = new NamedLocation{Name = nameof(ZeroLatitudeHalfMaxPositiveLongitude), Latitude = 0, Longitude = 90d};
            public static readonly NamedLocation ZeroLatitudeHalfMaxNegativeLongitude = new NamedLocation{Name = nameof(ZeroLatitudeHalfMaxNegativeLongitude), Latitude = 0, Longitude = -90d};
            public static readonly NamedLocation MaxPosititveLatitudeZeroLongitude = new NamedLocation{Name = nameof(MaxPosititveLatitudeZeroLongitude), Latitude = 90, Longitude = 0};
            public static readonly NamedLocation MaxNegativeLatitudeZeroLongitude = new NamedLocation{Name = nameof(MaxNegativeLatitudeZeroLongitude), Latitude = -90, Longitude = 0};

            public static IEnumerable<NamedLocation> Locations => new NamedLocation[]
            {
                NorthPole,
                SouthPole,
                Null,
                ZeroLatitudeHalfMaxNegativeLongitude,
                ZeroLatitudeHalfMaxPositiveLongitude,
                ZeroLatitudeMaxNegativeLongitude,
                ZeroLatitudeMaxPositiveLongitude,
                MaxNegativeLatitudeZeroLongitude,
                MaxPosititveLatitudeZeroLongitude,
            };

        }
    }


}

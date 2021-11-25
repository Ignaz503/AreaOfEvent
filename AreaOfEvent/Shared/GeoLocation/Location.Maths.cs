using System;

namespace AreaOfEvent.Shared.GeoLocation
{
    public partial class Location
    {
        public static class Maths
        {
            const double EarthRadiusMeters = 6371e3;

            public static double Distance( Location firstLocation, Location secondLocation )
            {

                /*
                 * https://www.movable-type.co.uk/scripts/latlong.html
                 * const R = 6371e3; // metres
                 * const φ1 = lat1 * Math.PI/180; // φ, λ in radians
                 * const φ2 = lat2 * Math.PI/180;
                 * const Δφ = (lat2-lat1) * Math.PI/180;
                 * const Δλ = (lon2-lon1) * Math.PI/180;
                 * const a = Math.sin(Δφ/2) * Math.sin(Δφ/2) +
                 *           Math.cos(φ1) * Math.cos(φ2) *
                 *           Math.sin(Δλ/2) * Math.sin(Δλ/2);
                 * const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
                 * const d = R * c; // in metres
                 */
                if (firstLocation.Latitude == secondLocation.Latitude && firstLocation.Longitude == secondLocation.Longitude)
                    return 0;

                var lat1Rad = firstLocation.Latitude * Mathematics.Maths.DegToRad;
                var lat2Rad = secondLocation.Latitude * Mathematics.Maths.DegToRad;

                var halfDifferenceLatRadians = ((secondLocation.Latitude - firstLocation.Latitude) * Mathematics.Maths.DegToRad) / 2d;
                var halfDifferenceLongRadians = ((secondLocation.Longitude - firstLocation.Longitude) * Mathematics.Maths.DegToRad) / 2d;

                var a = Math.Sin(halfDifferenceLatRadians) * Math.Sin(halfDifferenceLatRadians) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(halfDifferenceLongRadians)*Math.Sin(halfDifferenceLongRadians);

                var c =2 * Math.Atan2(Math.Sqrt(a),Math.Sqrt(1-a));

                return EarthRadiusMeters * c;
            }

            public static double BearingInDegrees( Location startPoint, Location endPoint )
            {
                /*
                 * https://www.movable-type.co.uk/scripts/latlong.html
                 * const y = Math.sin(λ2-λ1) * Math.cos(φ2);
                 * const x = Math.cos(φ1)*Math.sin(φ2) -
                 *           Math.sin(φ1)*Math.cos(φ2)*Math.cos(λ2-λ1);
                 * const θ = Math.atan2(y, x);
                 * const brng = (θ*180/Math.PI + 360) % 360; // in degrees
                 */


                if (startPoint.Latitude == endPoint.Latitude && startPoint.Longitude == endPoint.Longitude)
                    return 0d;

                var diffLongRad = (endPoint.Longitude - startPoint.Longitude) * Mathematics.Maths.DegToRad;

                var latEndRad = endPoint.Latitude * Mathematics.Maths.DegToRad;

                var y = Math.Sin(diffLongRad) * Math.Cos(latEndRad);

                var latStartRad = startPoint.Latitude * Mathematics.Maths.DegToRad;

                var x = Math.Cos(latStartRad) * Math.Sin(latEndRad)
                        - Math.Sin(latStartRad)* Math.Cos(latEndRad) * Math.Cos(diffLongRad);

                var phi = Math.Atan2(y,x);
                return ((phi * Mathematics.Maths.RadToDeg) + 360) % 360;
            }
        }

    }
}

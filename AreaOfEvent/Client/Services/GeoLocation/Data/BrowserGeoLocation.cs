using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Services.GeoLocation.Data
{
    public struct BrowserGeoLocation
    {
        public enum Status
        {
            Valid = 0,
            NotSupported =1,
            NoPermission = 2,
            Unavailable = 3,
            TimeOut = 4,
            UnknownError = 5
        }

        public Status Code { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public bool IsValid => Code == Status.Valid;

    }
}

using AreaOfEvent.Shared.GeoLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars
{
    public interface IGeoLocateable
    {
        Location GeoLocation { get; }
    }

    public class TestLocateable : IGeoLocateable
    {
        public Location GeoLocation { get; set; }
    }
    public class TestNontLocateable
    {
        public Location GeoLocation => throw new NotImplementedException();
    }
}

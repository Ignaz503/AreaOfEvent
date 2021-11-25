using AreaOfEvent.Server.Models.Organizers;
using AreaOfEvent.Server.Models.Sponsors;
using AreaOfEvent.Shared.GeoLocation;
using AreaOfEvent.Shared.Model.Events.Categories;
using AreaOfEvent.Shared.Model.Events.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Models.Events
{
    public class BaseEvent
    {
        public Guid ID { get; set; }

        public Category Category { get; set; }

        public Location GeoLocation { get; set; }

        public string Description { get; set; }

        public EventTimeFrame Date { get; set; }

        public Organizer Organizer { get; set; }

        public SponsorCollection SponsorsCollection { get; set; }
    }
}

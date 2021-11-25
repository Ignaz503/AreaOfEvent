using System;

namespace AreaOfEvent.Shared.Model.Events.Timing
{
    public class TimeSpan : EventTimeFrame
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}

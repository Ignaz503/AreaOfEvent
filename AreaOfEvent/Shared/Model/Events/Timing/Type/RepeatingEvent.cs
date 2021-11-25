using System;

namespace AreaOfEvent.Shared.Model.Events.Timing.Type
{
    public class RepeatingEvent : EventType
    {
        public DateTime RepeatFrequency { get; set; }
    }

}

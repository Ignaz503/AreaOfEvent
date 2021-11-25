using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Model.Events.Timing.Type
{
    public abstract class EventType
    {
        public static readonly EventType Default = new OneOffEvent();
    
    }
}

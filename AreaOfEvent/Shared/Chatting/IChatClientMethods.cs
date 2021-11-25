using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Chatting
{
    public interface IChatClientMethods
    {
        Task ReceiveMessage( string userName, string message );
    }
}

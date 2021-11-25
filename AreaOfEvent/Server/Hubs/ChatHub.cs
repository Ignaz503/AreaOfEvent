using AreaOfEvent.Shared.Chatting;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Hubs
{
    public class ChatHub : Hub<IChatClientMethods>, IChatServerMethods
    {
        public async Task SendMessage( string user, string message )
        {
            await Clients.All.ReceiveMessage( user, message );
        }

    }
}

using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Hubs
{
    public class Room4Hub : Hub
    {
        public async Task SendMessage(string user, string message, string profile)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, profile);
        }
    }
}

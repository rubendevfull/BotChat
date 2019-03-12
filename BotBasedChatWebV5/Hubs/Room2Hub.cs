using BotBasedChatWebV5.Application.Queries.Messages;
using BotBasedChatWebV5.Services.MessageBroker;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Hubs
{
    public class Room2Hub : Hub
    {
        public IMessageQueries _messageQueries { get; set; }
        public IMessageBroker _messagebroker { get; set; }
        public Room2Hub()
        {
            _messageQueries = (IMessageQueries)Startup.__serviceProvider.GetService(typeof(IMessageQueries));
            _messagebroker = (IMessageBroker)Startup.__serviceProvider.GetService(typeof(IMessageBroker));
        }


        public async Task SendMessage(string user, string message, string profile)
        {
            await _messagebroker.ManageMessageAsync(user, message, profile, Clients, 2);
        }
    }
}

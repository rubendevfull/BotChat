using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Services.MessageBroker
{
    public interface IMessageBroker
    {
        Task ManageMessageAsync(string user, string message, string profile, IHubCallerClients hub, int room);
    }
}

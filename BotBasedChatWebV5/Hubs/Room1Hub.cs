using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure;
using BotBasedChatInfrastructure.Repositories;
using BotBasedChatWebV5.Application.Commands;
using BotBasedChatWebV5.Application.Commands.Message;
using BotBasedChatWebV5.Application.Queries.Messages;
using BotBasedChatWebV5.Services.MessageBroker;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Hubs
{
    public class Room1Hub : Hub
    {
        public IMessageQueries _messageQueries { get; set; }
        public IMessageBroker _messagebroker { get; set; }
        public Room1Hub()
        {
            _messageQueries = (IMessageQueries)Startup.__serviceProvider.GetService(typeof(IMessageQueries));
            _messagebroker = (IMessageBroker)Startup.__serviceProvider.GetService(typeof(IMessageBroker));
        }
       

        public async Task SendMessage(string user, string message, string profile)
        {

            //var command = new MessageAddCommand(user, profile, message, 1);            
            //var identifiedCommand = new IdentifiedCommand<MessageAddCommand, int>(command, new Guid());
            //var resultCommand = await _mediator.Send(command);
            //if(resultCommand > 0)
            await _messagebroker.ManageMessageAsync(user, message, profile, Clients, 1);

            //await Clients.All.SendAsync("ReceiveMessage", user, message, profile);
        }
    }
}

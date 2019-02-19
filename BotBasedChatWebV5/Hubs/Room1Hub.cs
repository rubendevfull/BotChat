using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure.Repositories;
using BotBasedChatWebV5.Application.Commands;
using BotBasedChatWebV5.Application.Commands.Message;
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
        //private readonly IMessageRepository _repo;        
        //public Room1Hub(IMessageRepository repo)
        //{
        //    _repo = repo;
        //}
        public async Task SendMessage(string user, string message, string profile)
        {
            
            var command = new MessageAddCommand(user, profile, message, 1);            
            //var identifiedCommand = new IdentifiedCommand<MessageAddCommand, int>(command, new Guid());
            //var resultCommand = await _mediator.Send(command);
            //if(resultCommand > 0)
            await Clients.All.SendAsync("ReceiveMessage", user, message, profile);
        }
    }
}

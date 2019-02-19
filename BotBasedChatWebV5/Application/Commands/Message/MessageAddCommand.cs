using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Application.Commands.Message
{
    public class MessageAddCommand : IRequest<int>
    {
        public string User { get; private set; }
        public string Profile { get; private set; }
        public string Message { get; private set; }
        public int Room { get; private set; }

        public MessageAddCommand(string user, string profile, string message, int room)
        {
            User = user;
            Profile = profile;
            Message = message;
            Room = room;
        }
    }
}

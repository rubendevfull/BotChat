using BotBaseChatDomain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotBaseChatDomain.MessagesAggregate
{
    public class Message : Entity
    {
        public string Data { get; private set; }
        public int Room { get; private set; }
        public string UserName { get; private set; }
        public string ProfileUser { get; private set; }

        public Message(string data, int room, string userName, string profileUser)
        {
            Data = data;
            Room = room;
            UserName = userName;
            ProfileUser = profileUser;
        }
    }
}

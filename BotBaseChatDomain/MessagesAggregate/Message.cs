using BotBaseChatDomain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotBaseChatDomain.MessagesAggregate
{
    public class Message : Entity
    {
        public string Data { get; set; }
        public int Room { get; set; }
    }
}

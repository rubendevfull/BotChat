using System;
using System.Collections.Generic;
using System.Text;

namespace BotBaseChatDomain.MessagesAggregate
{
    public interface IMessageRepository
    {
        Message Add(Message msg);
    }
}

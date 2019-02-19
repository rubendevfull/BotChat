using BotBaseChatDomain.MessagesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Application.Queries.Messages
{
    public interface IMessageQueries
    {
        Task<IEnumerable<Message>> GetMessagesByRoom(int roomId, bool isTracked);
    }
}

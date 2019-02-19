using BotBaseChatDomain.MessagesAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotBasedChatInfrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDataContext _context;

        public MessageRepository(AppDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Message Add(Message msg)
        {
            return _context.Messages.Add(msg).Entity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace BotBasedChatWebV5.Application.Queries.Messages
{
    public class MessageQueries : IMessageQueries
    {
        private readonly AppDataContext _ctx;

        public MessageQueries(AppDataContext ctx)
        {
            _ctx = ctx;
            _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<IEnumerable<Message>> GetMessagesByRoom(int roomId, bool isTracked)
        {
            if (isTracked)
                _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            return await _ctx.Messages
                .Where(x => x.Room == roomId)
                .OrderByDescending(x => x.CreatedDate)
                .Take(50)
                .ToListAsync();
            
        }
    }
}

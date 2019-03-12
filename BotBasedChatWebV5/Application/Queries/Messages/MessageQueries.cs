using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BotBasedChatWebV5.Application.Queries.Messages
{
    public class MessageQueries : IMessageQueries
    {
        public IConfiguration Configuration { get; }
        private readonly AppDataContext _ctx;

        public MessageQueries(AppDataContext ctx, IConfiguration configuration)
        {
            Configuration = configuration;
            _ctx = ctx;
            //_ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<IEnumerable<Message>> GetMessagesByRoom(int roomId, bool isTracked)
        {
            var defaultValue = 50;
            var limitMessages = Int32.TryParse(Configuration["stackMessages"].ToString(), out defaultValue);
            if (isTracked)
                _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            var reverseOrder = await _ctx.Messages
                .Where(x => x.Room == roomId)
                .OrderByDescending(x => x.CreatedDate)
                .Take(defaultValue)
                .ToListAsync();

            return reverseOrder.OrderBy(x => x.CreatedDate).ToList();
        }
    }
}

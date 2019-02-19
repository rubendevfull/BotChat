using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotBaseChatDomain.MessagesAggregate;

namespace BotBasedChatInfrastructure
{
    public class AppDataContextSeed
    {
        private AppDataContext _ctx;

        public async Task SeedAsync(AppDataContext ctx)
        {
            _ctx = ctx;
            if (!_ctx.Messages.Any())
            {
                _ctx.Messages.AddRange(GetMessagesDemo());
                await _ctx.SaveChangesAsync();
            }
        }

        private List<Message> GetMessagesDemo()
        {
            return new List<Message>()
            {
                new Message("Message Test 1", 1, "rubenandrade201@gmail.com", "ruben201")
            };
        }
    }
}

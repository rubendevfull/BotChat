using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotBasedChatInfrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly AppDataContext _context;

        public RequestManager(AppDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            var request = await _context.
                FindAsync<IdempotencyTransactionLog>(id);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var exists = await ExistAsync(id);

            var request = exists ?
                throw new Exception($"Request with {id} already exists") :
                new IdempotencyTransactionLog()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }
    }
}

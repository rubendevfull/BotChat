using System;
using System.Collections.Generic;
using System.Text;

namespace BotBasedChatInfrastructure.Idempotency
{
    public class IdempotencyTransactionLog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}

using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure.Idempotency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Application.Commands.Message
{
    public class MessageAddCommandHandler : IRequestHandler<MessageAddCommand, int>
    {
        private readonly IMessageRepository _messageRepository;
        public MessageAddCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<int> Handle(MessageAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var messageDomain = new BotBaseChatDomain.MessagesAggregate.Message(request.Message,
                request.Room, request.User, request.Profile);

                var result = _messageRepository.Add(messageDomain);
                return result.Id;
            }
            catch (Exception e)
            {
                return 0;
            }            
        }
    }

    public class MessageAddIdentifiedCommandHandler : IdentifiedCommandHandler<MessageAddCommand, int>
    {
        public MessageAddIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override int CreateResultForDuplicateRequest()
        {
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatWebV5.Application.Commands;
using BotBasedChatWebV5.Application.Commands.Message;
using BotBasedChatWebV5.Model;
using BotBasedChatWebV5.Services.Csv;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BotBasedChatWebV5.Services.MessageBroker
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IMessageRepository _messageRepository;
        //private readonly IMediator _mediator;
        private readonly ICsvBot _csvBot;
        public MessageBroker(ICsvBot csvBot, IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            //_mediator = mediator;
            _csvBot = csvBot;
        }
        public async Task ManageMessageAsync(string user, string message, string profile, IHubCallerClients hub, int room)
        {
            var rpChecking = CheckMessageFormat(message);
            if (rpChecking.Status)
            {
                var rpCsvProcess = await _csvBot.GetCsvAsync((string)rpChecking.Data);
                if(rpCsvProcess.Status)
                {
                    var record = rpCsvProcess.Data as CsvResponseModel;                    
                    //await hub.All.SendAsync("ReceiveMessage", user, message, profile);
                    await hub.All.SendAsync("ReceiveMessage", 
                        user,
                        string.Format("{0} quote is ${1} per share", rpChecking.Data, record.Close),
                        "Csv Bot");
                }
                else
                {
                    await hub.All.SendAsync("ReceiveMessage", user,
                        string.Format("bad stock '{0}' identifier", rpChecking.Data),
                        "Csv Bot");
                }
            }
            else
            {
                var messageDomain = new Message(message,room, user, profile);
                var result = _messageRepository.Add(messageDomain);                 
                await hub.All.SendAsync("ReceiveMessage", user, message, profile);
            }                                       
        }

        public ResponseBasicVm CheckMessageFormat(string message)
        {
            var rp = new ResponseBasicVm();
            try
            {
                if(message.Contains("="))
                {
                    var tokens = message.Split("=");
                    if(tokens != null && tokens.Count() == 2)
                    {
                        var token1 = tokens[0];
                        if(token1 == "/stock")
                        {
                            var token2 = tokens[1].Trim().Replace("\u200B", string.Empty);
                            rp.Status = Regex.IsMatch(token2, @"^[a-zA-Z]+$");
                            rp.Data = token2;
                        }
                    }
                }                
            }
            catch (Exception)
            {
                rp.Status = false;
            }
            return rp;
        }
    }
}

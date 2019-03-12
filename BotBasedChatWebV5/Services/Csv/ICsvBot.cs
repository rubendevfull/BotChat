using BotBasedChatWebV5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Services.Csv
{
    public interface ICsvBot
    {
        Task<ResponseBasicVm> GetCsvAsync(string token);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotBasedChatWebV5.Model
{
    public class ResponseBasicVm
    {
        public bool Status { get; set; }
        public List<string> MsgGood { get; set; }
        public List<string> MsgBad { get; set; }

        public object Data { get; set; }

        public ResponseBasicVm()
        {
            MsgGood = new List<string>();
            MsgBad = new List<string>();
        }
    }
}

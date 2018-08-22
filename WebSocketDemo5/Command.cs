using System;
using System.Collections.Generic;
using System.Text;

namespace WebSocketDemo5
{
    public class Command
    {
        public CommandType Type { get; set; }


        public (string, string, string) Data { get; set; }
    }
}

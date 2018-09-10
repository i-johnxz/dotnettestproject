using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSocketDemo5
{
    public class CommandHandler
    {
        public (bool, Command) Parse(string cmd)
        {
            try
            {
                if (cmd.StartsWith("#"))
                {
                    var segment = cmd.Split(new[] { ' ' });

                    if (segment.Length > 0)
                    {
                        switch (segment[0])
                        {
                            case "#list": return (true, new Command() {Type = CommandType.List, Data = ("", "", "")});
                            case "#quit": return (true, new Command() {Type = CommandType.Quit, Data = ("", "", "")});
                            case "#nick":
                                return (true, new Command() {Type = CommandType.Nick, Data = (segment[1], "", "")});
                            case "#talk":
                                return (true,
                                    new Command()
                                    {
                                        Type = CommandType.Send,
                                        Data = (segment[1], string.Join(" ", segment.Skip(2)), "")
                                    });
                            default:
                                return (false, null);
                        }
                    }
                }

                return (false, null);
            }
            catch (Exception e)
            {
                return (false, null);
            }
        }
    }
}

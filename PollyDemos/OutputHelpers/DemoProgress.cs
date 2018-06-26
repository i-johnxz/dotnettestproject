using System;
using System.Collections.Generic;
using System.Text;

namespace PollyDemos.OutputHelpers
{
    public class DemoProgress
    {
        public Statistic[] Statistics { get; }

        public ColoredMessage[] Messages { get; }

        public DemoProgress(Statistic[] statistics, ColoredMessage[] messages)
        {
            Statistics = statistics;
            Messages = messages;
        }


        public DemoProgress(Statistic[] statistics, ColoredMessage coloredMessage)
        {
            Statistics = statistics;
            Messages = new[] { coloredMessage };
        }
    }
}

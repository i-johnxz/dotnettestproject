using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using PollyDemos.OutputHelpers;

namespace PollyDemos
{
    public abstract class DemoBase
    {
        //public IConfiguration Configuration { get; }

        //protected DemoBase(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected bool TerminateDemosByKeyPress => (Configuration["TerminateDemosByKeyPress"] ?? String.Empty).Equals(
        //    Boolean.TrueString,
        //    StringComparison.InvariantCultureIgnoreCase);

        //public virtual string Description => $"[Description for demo {GetType().Name} not yet providerd.]";

        //public abstract Statistic[] LasteStatistics { get; }

        //public DemoProgress ProgressWithMessage(string message)
        //{
        //    return new DemoProgress(LasteStatistics, new ColoredMessage(message));
        //}

        //public DemoProgress ProgressWithMessage(string message, Color color)
        //{
        //    return new DemoProgress(LasteStatistics, new ColoredMessage(message, color));
        //}

        //public DemoProgress ProgressWithMessage(IEnumerable<ColoredMessage> messages)
        //{
        //    return new DemoProgress(LasteStatistics, messages);
        //}
    }
}

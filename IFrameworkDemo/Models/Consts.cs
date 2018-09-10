using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFrameworkDemo.Models
{
    public static class Consts
    {
        public static string FrameworkDemoAssemblyPrefixName = "IFrameworkDemo";
        public static string DemoEventTopic = "Demo.DomainEvent";
        public static string DemoEventSubscriber = "Demo.EventSubscriber";
        public static string DemoDomainEventSubscriberProvider = "DemoDomainEventSubscriberProvider";
        public static string DbContextPoolSize = "DbContextPoolSize";
    }
}

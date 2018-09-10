using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.Event;
using IFramework.Infrastructure;
using IFramework.Message;

namespace IFrameworkDemo.Models
{
    public class CreateBlogDomainEvent : AggregateRootEvent
    {
        public CreateBlogDomainEvent(string blogId, string blogTitle, string blogContent, DateTime blogCreateTime)
        : base(blogId)
        {
            BlogId = blogId;
            BlogTitle = blogTitle;
            BlogContent = blogContent;
            BlogCreateTime = blogCreateTime;
        }

        public string BlogId { get; set; }

        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }

        public DateTime BlogCreateTime { get; set; }
    }

    [Topic("Demo.DomainEvent")]
    public class AggregateRootEvent : IAggregateRootEvent
    {
        public AggregateRootEvent()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public AggregateRootEvent(object aggregateRootId)
            : this()
        {
            AggregateRootId = aggregateRootId;
            Key = aggregateRootId.ToString();
            CreatedTime = DateTime.Now; ;
        }

        public int Version { get; set; }

        public object AggregateRootId { get; }

        public string AggregateRootName { get; set; }

        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }

        public string Key { get; set; }
        public string[] Tags { get; set; }
    }
}

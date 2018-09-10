using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.Domain;

namespace IFrameworkDemo.Models
{
    public class BlogHistory : AggregateRoot
    {
        public string Id { get; set; }

        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }

        public DateTime BlogCreateTime { get; set; }

        public BlogHistory(string id, string blogTitle, string blogContent, DateTime blogCreateTime)
        {
            Id = id;
            BlogTitle = blogTitle;
            BlogContent = blogContent;
            BlogCreateTime = blogCreateTime;
        }
    }
}

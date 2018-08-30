using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IFramework.Domain;
using IFramework.Infrastructure;

namespace IFrameworkDemo.Models
{
    public class Blog: AggregateRoot
    {
        public string Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        public Blog(string title, string content)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Title = title;
            Content = content;
            OnEvent(new CreateBlogDomainEvent(Id, title, content, DateTime.Now));
        }
    }
}

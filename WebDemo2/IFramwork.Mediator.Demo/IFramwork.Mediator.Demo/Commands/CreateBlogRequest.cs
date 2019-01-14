using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramwork.Mediator.Demo.Models;
using MediatR;

namespace IFramwork.Mediator.Demo.Commands
{
    public class CreateBlogRequest: IRequest<string>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public CreateBlogRequest()
        {
            
        }

        public CreateBlogRequest(string title, string content)
        {
            Title = title;
            Content = content;
        }
        
    }
}

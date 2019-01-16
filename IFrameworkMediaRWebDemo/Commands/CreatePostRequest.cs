using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFrameworkMediaRWebDemo.Models;
using MediatR;

namespace IFrameworkMediaRWebDemo.Commands
{
    public class CreatePostRequest: CreatePostModel, IRequest<PostResult>
    {
    }
}

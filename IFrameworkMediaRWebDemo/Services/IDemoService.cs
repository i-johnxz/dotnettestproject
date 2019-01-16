using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFrameworkMediaRWebDemo.Models;

namespace IFrameworkMediaRWebDemo.Services
{
    public interface IDemoService
    {
        Task<PostResult> PostResult(CreatePostModel createPostModel);
    }
}

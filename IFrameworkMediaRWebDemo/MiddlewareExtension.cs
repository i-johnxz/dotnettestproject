using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.AspNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace IFrameworkMediaRWebDemo
{
    public static class MiddlewareExtension
    {
        
        public static IApplicationBuilder UseLogLevelController(this IApplicationBuilder app, string path = "/api/loglevels")
        {
            app.Map(PathString.FromUriComponent(path),
                configuration => { configuration.UseMiddleware<LogLevelControllerMiddleware>(); });
            return app;
        }
    }
}

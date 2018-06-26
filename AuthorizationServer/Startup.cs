using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            InMemoryConfiguration.Configuration = this._configuration;
            services.AddIdentityServer()
                    //.AddDeveloperSigningCredential()
                    .AddSigningCredential(new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _configuration["Certificates:CerPath"]), _configuration["Certificates:Password"]))
                    .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
                    .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
                    .AddInMemoryClients(InMemoryConfiguration.GetClients())
                    .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}

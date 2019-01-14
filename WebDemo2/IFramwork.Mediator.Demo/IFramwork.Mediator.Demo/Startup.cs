using System;
using IFramework.AspNet;
using IFramework.DependencyInjection;
using IFramework.DependencyInjection.Autofac;
using IFramework.JsonNet;
using IFramework.Log4Net;
using IFramwork.Mediator.Demo.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IFramwork.Mediator.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            IFramework.Config.Configuration.Instance.UseAutofacContainer()
                                                    .UseConfiguration(configuration)
                                                    .UseCommonComponents()
                                                    .UseJsonNet();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddMediatR();

            return ObjectProviderFactory.Instance
                .RegisterComponents(RegisterComponents, ServiceLifetime.Scoped)
                .Populate(services)
                .Build();
        }

        private static void RegisterComponents(IObjectProviderBuilder providerBuilder, ServiceLifetime lifetime)
        {
            providerBuilder.Register<IBlogService, BlogService>(lifetime);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.UseLog4Net(new Log4NetProviderOptions() {EnableScope = true});
            

            app.UseMvc();

            app.UseLogLevelController();

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.SetMinLevel(LogLevel.Information);
            logger.LogInformation($"Startup configured env: {env.EnvironmentName}");
        }
    }
}

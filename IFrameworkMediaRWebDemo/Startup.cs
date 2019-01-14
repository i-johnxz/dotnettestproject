using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IFramework.DependencyInjection;
using IFramework.DependencyInjection.Autofac;
using IFramework.JsonNet;
using IFramework.Log4Net;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IFrameworkMediaRWebDemo
{
    public class Startup
    {
        public const string AssemblyPrefixName = "IFrameworkMediaRWebDemo";

        public Startup(IConfiguration configuration)
        {
            var configurationInstance = IFramework.Config.Configuration.Instance;
            configurationInstance.UseAutofacContainer(a => a.GetName().Name.StartsWith(AssemblyPrefixName))
                .UseConfiguration(configuration)
                .UseCommonComponents()
                .UseJsonNet();
            //.UseLog4Net(new Log4NetProviderOptions() {EnableScope = true});

        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //services.AddMediatR();

            return ObjectProviderFactory.Instance
                .RegisterComponents(RegisterComponents, ServiceLifetime.Singleton)
                .Populate(services)
                .Build();
        }

        private static void RegisterComponents(IObjectProviderBuilder providerBuilder, ServiceLifetime lifetime)
        {


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net(new Log4NetProviderOptions { EnableScope = true });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();



            app.UseLogLevelController();
            var logger = loggerFactory.CreateLogger<Startup>();
            logger.SetMinLevel(LogLevel.Information);
            logger.LogError($"Startup configured env: {env.EnvironmentName}");

        }
    }
}

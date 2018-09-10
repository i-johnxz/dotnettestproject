using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.Config;
using IFramework.DependencyInjection;
using IFramework.DependencyInjection.Autofac;
using IFramework.EntityFrameworkCore;
using IFramework.JsonNet;
using IFramework.Log4Net;
using IFramework.Message;
using IFramework.MessageQueue;
using IFramework.MessageQueue.ConfluentKafka;
using IFramework.MessageStores.Relational;
using IFrameworkDemo.Controllers;
using IFrameworkDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IFrameworkDemo
{
    public class Startup
    {
       

        private static IMessageProcessor _domainEventProcessor;
        private static IMessagePublisher _messagePublisher;

        public Startup(IConfiguration configuration)
        {
            var configurationInstance = IFramework.Config.Configuration
                .Instance
                .UseAutofacContainer(a => a.GetName()
                    .Name
                    .StartsWith(Consts.FrameworkDemoAssemblyPrefixName))
                .UseConfiguration(configuration)
                .UseCommonComponents()
                .UseJsonNet()
                .UseRelationalMessageStore<DemoDbContext>()
                .UseMessagePublisher(Consts.DemoEventTopic)
                .UseEntityFrameworkComponents(typeof(RepositoryBase<>))
                .UseDbContextPool<DemoDbContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlServer(IFramework.Config.Configuration.Instance.GetConnectionString("DemoDb"));
                    },
                    IFramework.Config.Configuration.Instance.Get<int>(Consts.DbContextPoolSize));
            string kafkaBrokerList;
            kafkaBrokerList = configurationInstance[nameof(kafkaBrokerList)];
            configurationInstance.UseConfluentKafka(kafkaBrokerList);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return ObjectProviderFactory.Instance
                .RegisterComponents(RegisterComponents, ServiceLifetime.Scoped)
                .Populate(services)
                .Build();
        }

        /// <summary>
        ///     this method can provider more functionality of ObjectProviderBuild
        /// </summary>
        /// <param name="providerBuilder"></param>
        /// <param name="lifetime"></param>
        private static void RegisterComponents(IObjectProviderBuilder providerBuilder, ServiceLifetime lifetime)
        {
            providerBuilder.Register<IDemoRepository, DemoRepository>(lifetime);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.UseLog4Net(new Log4NetProviderOptions() {EnableScope = true});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<DemoDbContext>();
                    dbContext.EnsureSeed();
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            StartMessageQueueComponents();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }



        private void StartMessageQueueComponents()
        {

            #region event subscriber init

            _domainEventProcessor = MessageQueueFactory.CreateEventSubscriber(new[]
                {
                    new TopicSubscription(Consts.DemoEventTopic),
                },
                Consts.DemoEventSubscriber,
                Environment.MachineName,
                new[]
                {
                    Consts.DemoDomainEventSubscriberProvider
                });

            _domainEventProcessor.Start();

            #endregion



            #region EventPublisher init

            _messagePublisher = MessageQueueFactory.GetMessagePublisher();

            _messagePublisher.Start();

            #endregion
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using httpclientfactorysample.ClientService;
using httpclientfactorysample.Handlers;
using httpclientfactorysample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace httpclientfactorysample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            //services.AddHttpClient("github", c =>
            //{
            //    c.BaseAddress = new Uri("https://api.github.com/");
            //    c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json"); // Github API versioning
            //    c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample"); // Github requires a user-agent
            //});
            //services.AddHttpClient<GitHubService>();

            //services.AddHttpClient<RepoService>(c =>
            //{
            //    c.BaseAddress = new Uri("https://api.github.com/");
            //    c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //    c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            //});

            services.AddTransient<ValidateHeaderHandler>();
            services.AddTransient<SecureRequestHandler>();

            services
            .AddHttpClient("hello", c => { c.BaseAddress = new Uri("http://localhost:51150/"); })
            .AddTypedClient(Refit.RestService.For<IHelloClient>)
            .AddHttpMessageHandler<ValidateHeaderHandler>()
            .AddHttpMessageHandler<SecureRequestHandler>()
            ;
            //services.AddHttpClient("hello").AddTypedClient(Refit.RestService.For<IHelloClient>);
            //services.AddRefitClient
            //services.AddRefitClient<IHelloClient>().ConfigureHttpClient((provider, client) =>
            //    {
            //        client.BaseAddress = new Uri("");
            //    }).AddHttpMessageHandler<>()();
            //services.AddHttpClient<UnreliableEndpointCallerService>()

            //    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(2000)));

            //var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
            //var longTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30));

            //var registry = services.AddPolicyRegistry();
          

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

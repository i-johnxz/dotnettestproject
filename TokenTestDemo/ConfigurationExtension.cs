using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using TokenTestDemo.Services;

namespace TokenTestDemo
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddWangLianOpenApiService(this ServiceCollection services,
            Action<WangLianOpenApiInterfaceOption> setupAction = null)
        {
            services.Configure(setupAction ?? (options => {
                IFramework.Config.Configuration.Instance.GetSection(nameof(WangLianOpenApiInterfaceOption)).Bind(options);
            }));


            services.AddSingleton(typeof(IWangLianAuthenticateApi), provider =>
            {
                string wangLianOpenApiServiceBaseUri = provider.GetService<IOptions<WangLianOpenApiInterfaceOption>>()
                    .Value.WangLianOpenApiServiceBaseUri;

                var wangLianHttpClient = new HttpClient()
                {
                    BaseAddress = new Uri(wangLianOpenApiServiceBaseUri)
                };

                return RestService.For<IWangLianAuthenticateApi>(wangLianHttpClient);
            });

            services.AddSingleton(typeof(IWangLianOpenApi), provider =>
            {
                string wangLianOpenApiServiceBaseUri = provider.GetService<IOptions<WangLianOpenApiInterfaceOption>>().Value
                    .WangLianOpenApiServiceBaseUri;

                var wangLianHttpClient = new HttpClient()
                {
                    BaseAddress = new Uri(wangLianOpenApiServiceBaseUri)
                };

                return RestService.For<IWangLianOpenApi>(wangLianHttpClient);
            });




            return services;
        }
    }
}

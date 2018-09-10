using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Refit;

namespace RefitDemo3
{
    public static class ConfigurationExtension
    {

        private static void ConfigureOptions(BankCardOption options) => IFramework.Config.Configuration.Instance.GetSection(nameof(BankCardOption)).Bind(options);
        
        public static IServiceCollection AddBankCardServices(this IServiceCollection services,
            Action<BankCardOption> setupActions = null)
        {
            var bankCardOption = new BankCardOption();
            
            if (setupActions != null)
            {
                setupActions(bankCardOption);
            }
            else
            {
                ConfigureOptions(bankCardOption);
            }
            
            services.Configure(setupActions ?? ConfigureOptions);

            services.AddTransient<ApiHttpHandler>();

            services.AddRefitClient<IAliPayBankCardParseService>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(bankCardOption.CcdcapiUri))
                    .AddHttpMessageHandler<ApiHttpHandler>();
                


            
            

            //services.AddSingleton(typeof(IAliPayBankCardParseService),
            //    provider =>
            //    {
            //        string ccdcapiUri = provider.GetService<IOptions<BankCardOption>>().Value.CcdcapiUri;

            //        var adapterHttpclient = new HttpClient(new ApiHttpHandler(provider.GetService<ILoggerFactory>()
            //            ?.CreateLogger(typeof(IAliPayBankCardParseService))))
            //        {
            //            BaseAddress = new Uri(ccdcapiUri)
            //        };



            //        return RestService.For<IAliPayBankCardParseService>(adapterHttpclient);
            //    });
            return services;
        }
    }
}

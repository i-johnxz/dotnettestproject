using System;
using System.IO;
using System.Threading.Tasks;
using IFramework.DependencyInjection;
using IFramework.DependencyInjection.Autofac;
using IFramework.JsonNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace RefitDemo3
{
    public class UnitTest1
    {

        public UnitTest1()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var services = new ServiceCollection();

            IFramework.Config.Configuration.Instance
                .UseAutofacContainer()
                .UseCommonComponents()
                .UseConfiguration(builder.Build())
                .UseJsonNet();

            services
                .AddBankCardServices()
                .AddSingleton<IHostingEnvironment, IHostingEnvironment>();

            ObjectProviderFactory.Instance.Build(services);
        }

        [Fact]
        public async Task Test1()
        {
            using (var scope = ObjectProviderFactory.CreateScope())
            {
                var service = scope.GetService<IAliPayBankCardParseService>();
                var result = await service.ParseBankCardAsync(cardNo: "6217920159440572");
            }

        }
    }
}

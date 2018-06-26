using PollyDemos.OutputHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Foundatio.Messaging;
using PollyDemos.Async;
using WebApiClient;
using WebApiClient.Attributes;

namespace PollyTestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //IMessageBus messageBus = new InMemoryMessageBus();
            //messageBus.SubscribeAsync<SimpleMessageA>(msg => { Console.WriteLine(msg.Data); }).Wait();

            //messageBus.PublishAsync(new SimpleMessageA
            //{
            //    Data = "Hello"
            //}).Wait();

            var config = new HttpApiConfig()
            {
                HttpHost = new Uri("http://localhost:53247/"),
            };
            var apiInerceptor = new TestApiInterceptor(config);

            using (var client = HttpApiClient.Create(typeof(ITestWebApi), apiInerceptor) as ITestWebApi)
            {
                //var result = client.GetTest2Async().GetAwaiter().GetResult();
                //Console.WriteLine(result.ToString());

                //var result = client.AddTestAsync(new IdName("123", "Test")).GetAwaiter().GetResult();
                var result = client.TestPostAsync(new IdName("test", "test")).GetAwaiter();
                //Console.WriteLine("w");
            }

            //Assembly.GetAssembly(typeof(IHttpApi))
            //    .GetTypes()
            //    .Where(t => t != typeof(IHttpApi) && typeof(IHttpApi).IsAssignableFrom(t))
            //    .ForEach(serviceType =>
            //    {
            //        var client = HttpApiClient.Create(serviceType, config) as ITestWebApi; 

            //    });
            //var assemblys = Assembly.GetAssembly(typeof(IServiceHttpClient))
            //                        .GetTypes()
            //                        .Where(t => t != typeof(IServiceHttpClient) && typeof(IServiceHttpClient).IsAssignableFrom(t));

            //var client = HttpApiClient.Create(typeof(ITestWebApi), apiInerceptor) as ITestWebApi;
            //Console.WriteLine("End");
            //Console.Read();
        }


    }

    //[HttpHost("http://localhost:53247/")]
    public interface ITestWebApi : IServiceHttpClient
    {
        [HttpGet("api/Values/gettests2")]
        ITask<IdName> GetTest2Async();

        [HttpPost("api/Values/add")]
        ITask<string> AddTestAsync([JsonContent]IdName idName);

        [HttpGet("api/Values/check")]
        ITask<string> CheckAccountAsync(User user);

        [HttpGet("api/Values/testpost")]
        Task TestPostAsync([JsonContent]IdName idName);
    }

    public interface IServiceHttpClient : IHttpApi
    {

    }

    public class User
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public User()
        {
            
        }

        public User(string account, string password)
        {
            Account = account;
            Password = password;
        }
    }

    public class IdName
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IdName()
        {

        }

        public IdName(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }

    public static class CollectionUtils
    {
        public static bool NotEmpty<T>(IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }
        public static bool IsEmpty<T>(IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> func)
        {
            foreach (var c in collection)
            {
                var local = c;
                func.Invoke(local);
            }
        }

        public static decimal? SumNullable(this IEnumerable<decimal?> source)
        {
            if (source == null)
                return null;

            decimal? sum = null;
            foreach (var v in source)
            {
                if (v == null)
                    continue;
                sum = sum ?? 0;

                sum += v;
            }

            return sum;
        }

        public static decimal? SumNullable<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return source.Select(x => selector(x)).SumNullable();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Nest;
using Xunit.Abstractions;

namespace elasticsearchdemo
{
    public class BaseTest
    {
        protected readonly ITestOutputHelper Output;
        protected static readonly Uri Uri = new Uri("");

        protected readonly ElasticClient Client = new ElasticClient(new ConnectionSettings(Uri).EnableHttpCompression()
            .DisableDirectStreaming().PrettyJson().RequestTimeout(TimeSpan.FromMinutes(2)));


        public BaseTest(ITestOutputHelper output)
        {
            Output = output;
        }
    }
}

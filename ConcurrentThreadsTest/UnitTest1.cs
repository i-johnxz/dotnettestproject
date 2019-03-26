using System;
using Xunit;
using Xunit.Abstractions;

namespace ConcurrentThreadsTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;
        
        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test1()
        {
            var url = new Uri("/");
            _output.WriteLine(url.AbsoluteUri);
        }
    }
}

using System;
using Snowflake.Core;

namespace SnowflakeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new IdWorker(1, 1, 1);
            long id = worker.NextId();
            Console.WriteLine(id);
            Console.Read();
        }
    }
}

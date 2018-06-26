using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace TestQueueConsole
{
    class Program
    {

        public static string ConnectionString =>
            @"Server=(localdb)\projects;Database=TestDb;Integrated Security=true;MultipleActiveResultSets=True;";

        protected static IDbConnection _connection;

        protected static IDbConnection MarsConnection => _connection ?? (_connection = GetOpenConnection());


        public static SqlConnection GetOpenConnection(bool mars = false)
        {
            var cs = ConnectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }

            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        static void Main(string[] args)
        {
            TestBatch().ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");
        }


        public static async Task TestBatch()
        {
            var batch = 3; //并行执行的task数
            var tasks = new List<Task>();

            var idNames =
                await MarsConnection.QueryAsync<IdName>(new CommandDefinition("select * from temptb",
                    flags: CommandFlags.Pipelined));

            var args = new ConcurrentQueue<IdName>(idNames);//args就相当于所有的接口

            for (int i = 0; i < batch; i++)
            {
                var task = Task.Run(async () =>
                {
                    while (true)
                    {
                        if (args.TryDequeue(out var arg))
                        {
                            await DoAsync(arg); //具体执行的业务逻辑
                        }
                        else
                        {
                            break;
                        }
                    }

                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public static async Task DoAsync(IdName arg)
        {
            await MarsConnection.ExecuteAsync(new CommandDefinition(@"update temptb
                                                                      set Enable = @Enable
                                                                      where Id = @Id",
                new
                {
                    Enable = true,
                    arg.Id
                },
                flags: CommandFlags.Pipelined));
        }

        public class IdName
        {
            public IdName()
            {

            }

            public IdName(string id, string name)
            {
                Id = id;
                Name = name;
            }

            public string Id { get; set; }

            public string Name { get; set; }

        }
    }
}

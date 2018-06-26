using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace TestDapper
{
    public class TestQueue
    {

        private readonly ITestOutputHelper _output;


        public static string ConnectionString =>
            @"Server=(localdb)\projects;Database=TestDb;Integrated Security=true;MultipleActiveResultSets=True;";

        protected IDbConnection _connection;

        protected IDbConnection MarsConnection => _connection ?? (_connection = GetOpenConnection());


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
        public TestQueue(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task TestInsert()
        {
            var idNames = Enumerable.Range(1, 20000)
                .Select(id => new IdName(Guid.NewGuid().ToString(), RandomString(id))).ToArray();


            int count = await MarsConnection.ExecuteAsync(new CommandDefinition("insert temptb (Id,Name) values(@Id, @Name)", idNames, flags: CommandFlags.Pipelined));

            Assert.True(count > 0);
        }

        [Fact]
        public async Task Test_InsertAll()
        {
            var idNames = Enumerable.Range(1, 20000)
                .Select(id => new IdName(Guid.NewGuid().ToString(), RandomString(id))).ToArray();

            var result = await MarsConnection.InsertAsync(idNames);

            Assert.True(result > 0);
        }


        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
        private string RandomString(int size)
        {

            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        //[Fact]
        //public Task TestBatchQueue()
        //{

        //}

        [Fact]
        public async Task Test_GetIdNames()
        {
            //var results =
            //    await MarsConnection.QueryAsync<IdName>(new CommandDefinition("select * from temptb",
            //        flags: CommandFlags.Pipelined));
            //foreach (var result in results)
            //{
            //    _output.WriteLine(result.Name);
            //}
            //Assert.NotNull(results);

            await MarsConnection.ExecuteAsync(new CommandDefinition(@"update temptb
                                                                      set Enable = @Enable
                                                                      where Id = @Id",
                new
                {
                    Enable = true,
                    Id = "0000ab33-48aa-4ecc-8690-b21a2913bb74"
                },
                flags: CommandFlags.Pipelined));
        }

        [Fact]
        public async Task Test_DapperUpdate()
        {
            var idNames =
            (await MarsConnection.QueryAsync<IdName>(new CommandDefinition("select * from temptb",
                flags: CommandFlags.Pipelined))).ToArray();
            foreach (var idName in idNames)
            {
                idName.Enable = !idName.Enable;
            }

            var result = await MarsConnection.UpdateAsync(idNames);
            Assert.True(result);
        }

        [Fact]
        public async Task Test_SqlUpdate()
        {
            var ids = (await MarsConnection.QueryAsync<IdName>("select * from temptb")).ToArray();
            foreach (var idName in ids)
            {
                idName.Enable = !idName.Enable;
            }

            var result = await MarsConnection.ExecuteAsync(new CommandDefinition(@"update temptb 
set [Name] = @Name, 
[Enable] = @Enable 
where [Id] = @Id", ids, flags: CommandFlags.Pipelined));
            Assert.True(result > 0);
        }

        [Fact]
        public async Task TestBatch()
        {
            var batch = 4; //并行执行的task数
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

        public async Task DoAsync(IdName arg)
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

        void GetData()
        {
            (_, int population, _) = CityData("Munich");
        }

        (string name, int population, double square) CityData(string name)
        {
            
            if (name == "Munich")
            {
                var cityData = (name, 1542886, 310.4);
                return cityData;
            }

            return ("", 0, 0);
        }
    }

    [Table("temptb")]
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
        
        [ExplicitKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Enable { get; set; }
    }
}


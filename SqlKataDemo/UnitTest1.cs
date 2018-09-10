using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MessagePack;
using MessagePack.Resolvers;
using Newtonsoft.Json;
using SqlKata;
using SqlKata.Compilers;
using Xunit;
using Xunit.Abstractions;

namespace SqlKataDemo
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _iOutput;

        private readonly SqlServerCompiler _compiler = new SqlServerCompiler
        {
            UseLegacyPagination = false
        };

        private readonly IDbConnection _connection = new SqlConnection(@"Server=(localdb)\projects;Database=Asset;Integrated Security=true;Enlist=false");

        public UnitTest1(ITestOutputHelper iOutput)
        {
            _iOutput = iOutput;
        }

        [Fact]
        public void Test_SqlKata()
        {
            
            var query = new Query("Users").Where("Id", 1).Where("Status", "Active");

            SqlResult result = _compiler.Compile(query);


            
            _iOutput.WriteLine(result.Sql);
        }

        [Fact]
        public void Test_Compilers()
        {
           

            var query = new Query("Posts").Limit(10).Offset(20);

            SqlResult result = _compiler.Compile(query);

            _iOutput.WriteLine(result.RawSql);
        }

        [Fact]
        public void Test_Column()
        {
            var query = new Query("Posts").Select("Id", "Title", "CreatedAt as Date");

            SqlResult result = _compiler.Compile(query);

            _iOutput.WriteLine(result.RawSql);
        }

        [Fact]
        public async Task Test_DapperAndSqlKata()
        {
            var startDate = DateTime.Now;
            var query = new Query("Loans").Where("Id", 20000584).Select("Id", "CreatedTime", "CreatorId", "Status");

            SqlResult result = _compiler.Compile(query);

            var loan = await _connection.QueryFirstOrDefaultAsync<LoanInfo>(result.Sql,
                result.NamedBindings);

            var endDate = DateTime.Now;

            _iOutput.WriteLine((endDate - startDate).TotalSeconds.ToString(CultureInfo.InvariantCulture));
            _iOutput.WriteLine(MessagePackSerializer.ToJson(loan));
        }

        [Fact]
        public async Task Test_Dapper2()
        {
            var startDate = DateTime.Now;

            var loan = await _connection.QueryFirstOrDefaultAsync<LoanInfo>(@"select Id, CreatedTime, CreatorId,Status
from Loans
where id = @id", new {id = 20000584});

            var endDate = DateTime.Now;

            _iOutput.WriteLine((endDate - startDate).TotalSeconds.ToString(CultureInfo.InvariantCulture));
            _iOutput.WriteLine(MessagePackSerializer.ToJson(loan));
        }
    }

    public class Post
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class LoanInfo
    {
        public long Id { get; set; }

     
        public DateTime CreatedTime { get; set; }

     
        public long CreatorId { get; set; }

        public string Status { get; set; }
    }
}

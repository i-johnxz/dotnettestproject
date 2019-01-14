using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Xunit;

namespace TestDapper
{
    public class TestMulitipe
    {
        [Fact]
        public void Test()
        {
            using (var con = new SqlConnection("server=10.100.3.110,60005;User ID=sa;Password=sa123.com;database=p2p;Enlist=false"))
            {
                Func<Test1, Test2, Test3, Test1> map = (test1, test2, test3) =>
                {
                    test1.Test2 = test2;
                    test1.Test3 = test3;
                    return test1;
                };

                var splitOn = "Id,Id";

                var result = con.Query(
                    "select '1' as 'Id', 'Test' as 'Name', null as 'Id', 'Test2' as 'Name', '3' as 'Id', 'Test3' as 'Name' ",
                    map, splitOn: splitOn);
            }
        }
    }

    public class Test1
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Test2 Test2 { get; set; }

        public Test3 Test3 { get; set; }
    }

    public class Test2
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class Test3
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

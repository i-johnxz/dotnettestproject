using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Xunit;
using Xunit.Abstractions;

namespace TestDapper
{
    public class QueryAsyncTest
    {
        private readonly ITestOutputHelper _output;

        private const string ConnectionString = @"Server=10.100.7.22,60005;Database=Asset;User ID=asset;Password=Asset123.com;Enlist=false";
            //@"Server=(localdb)\projects;Database=Asset;Integrated Security=true;Enlist=false;";

        public QueryAsyncTest(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public async Task Test_QueryAsync()
        {
            await Test_QueryAssetBrokerInfoAsync();
            //await Test_QueryFundingBrokerInfoAsync();
        }

        [Fact]
        public async Task Test_QueryAssetBrokerInfoAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                const string sql = @"select Id as 'BrokerId', Name, Code
							from AssetBrokers where deleted = 0 
							order by CreatedTime desc";


                DataProviderExtension.SetTypeMap(typeof(AssetBrokerInfo));

                

                var brokers = await connection.QueryAsync<AssetBrokerInfo>(sql).ConfigureAwait(false);
                var result = brokers.ToArray();


                foreach (var assetBrokerInfo in result)
                {
                    _output.WriteLine(assetBrokerInfo.Name);
                }

            }
        }


        public async Task Test_QueryFundingBrokerInfoAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                const string sql = @"select Id as 'BrokerId', Name, Code
							from FundingBrokers
							where deleted = 0 ";


                var brokers = await connection.QueryAsync<BrokerInfo>(sql).ConfigureAwait(false);
                var result = brokers.ToArray();


                foreach (var fundingBrokerInfo in result)
                {
                    _output.WriteLine(fundingBrokerInfo.Name);
                }

            }
        }


        
    }



    public class TypeHandler : SqlMapper.TypeHandler<AssetBrokerInfo>
    {
        public override void SetValue(IDbDataParameter parameter, AssetBrokerInfo value)
        {
            parameter.Value = value.ToString();
        }

        public override AssetBrokerInfo Parse(object value)
        {
            return new AssetBrokerInfo();
        }
    }


    public class AssetBrokerInfo : BrokerInfo
    {
        public string AppId { get; set; }
    }

    public class BrokerInfo
    {

        public long? BrokerId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        

    }
    
}

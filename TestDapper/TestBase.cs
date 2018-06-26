using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Dapper;
using Xunit;

namespace TestDapper
{
    public abstract class TestBase : IDisposable
    {

        public static string ConnectionString =>
            @"Server=(localdb)\projects;Database=tempdb;Integrated Security=true;";

        protected SqlConnection _connection;

        protected SqlConnection connection => _connection ?? (_connection = GetOpenConnection());


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

        public SqlConnection GetClosedConnection()
        {
            var cs = ConnectionString;
            var scsb = new SqlConnectionStringBuilder(cs)
            {
                MultipleActiveResultSets = true
            };
            cs = scsb.ConnectionString;
            var conn = new SqlConnection(cs);
            if(conn.State != ConnectionState.Closed) throw new InvalidOperationException("Should be closed!");
            return conn;
        }


        protected static CultureInfo ActiveCulture
        {
#if NETCOREAPP2_0
            get { return CultureInfo.CurrentCulture;}
            set { CultureInfo.CurrentCulture = value; }
#else
            get { return Thread.CurrentThread.CurrentCulture; }
            set { Thread.CurrentThread.CurrentCulture = value; }
#endif

        }


        static TestBase()
        {
            Console.WriteLine("Dapper: " + typeof(SqlMapper).AssemblyQualifiedName);
            Console.WriteLine("Using Connectionstring: {0}", ConnectionString);
#if NETCOREAPP2_0
            Console.WriteLine("CoreCLR (NETCOREAPP2_0)");
#else
            Console.WriteLine(".NET: " + Environment.Version);
            Console.Write("Loading native assemblies for SQL types...");
            try
            {
                SqlServerTypesLoader.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                Console.WriteLine("done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed.");
                Console.Error.WriteLine(ex.Message);
            }
#endif
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }

    [CollectionDefinition(Name, DisableParallelization = true)]
    public class NonParallelDefinition : TestBase
    {
        public const string Name = "NonParallel";
    }
}

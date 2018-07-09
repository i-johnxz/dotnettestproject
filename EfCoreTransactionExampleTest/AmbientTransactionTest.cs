using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EfCoreTransactionExampleTest
{
    public class AmbientTransactionTest
    {
        [Fact]
        public void Test()
        {
            var connectionString = @"Server=(localdb)\projects;Database=EFGetStarted.NewDb;Integrated Security=true;";


            using (var context = new BloggingContext(
                new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlServer(connectionString)
                    .Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();

                try
                {
                    // Run raw ADO.NET command in the transaction
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM dbo.Blogs";
                    command.ExecuteNonQuery();

                    // Run an EF Core command in the transaction
                    var options = new DbContextOptionsBuilder<BloggingContext>()
                        .UseSqlServer(connection)
                        .Options;

                    using (var context = new BloggingContext(options))
                    {
                        context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
                        context.SaveChanges();
                    }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    scope.Complete();
                }
                catch (System.Exception e)
                {
                    throw e;
                    // TODO: Handle failure
                }
            }
        }
    }
}

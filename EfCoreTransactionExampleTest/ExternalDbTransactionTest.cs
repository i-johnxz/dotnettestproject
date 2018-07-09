using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EfCoreTransactionExampleTest
{
    public class ExternalDbTransactionTest
    {
        [Fact]
        public void Test()
        {
            var connectionString = @"Server=(localdb)\projects;Database=EFGetStarted.NewDb;Integrated Security=true;";

            using (var context = new BloggingContext(new DbContextOptionsBuilder<BloggingContext>()
                .UseSqlServer(connectionString).Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            #region Transaction

            var connection = new SqlConnection(connectionString);
            connection.Open();


            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = "DELETE FROM Blogs";
                    command.ExecuteNonQuery();

                    var options = new DbContextOptionsBuilder<BloggingContext>()
                        .UseSqlServer(connection)
                        .Options;

                    using (var context = new BloggingContext(options))
                    {
                        context.Database.UseTransaction(transaction);
                        context.Blogs.Add(new Blog {Url = "http://blogs.msdn.com/dotnet"});
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            #endregion
        }
    }
}

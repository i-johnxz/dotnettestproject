using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace EfCoreTransactionExampleTest
{
    public class SharingTransactionTest
    {
        [Fact]
        public void Test()
        {
            var connectionString = @"Server=(localdb)\projects;Database=EFGetStarted.NewDb;Integrated Security=true;";

            using (var context = new BloggingContext(new DbContextOptionsBuilder<BloggingContext>().UseSqlServer(connectionString).Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }


            #region Transaction

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseSqlServer(new SqlConnection(connectionString))
                .Options;

            using (var context1 = new BloggingContext(options))
            {
                using (var transaction = context1.Database.BeginTransaction())
                {
                    try
                    {
                        context1.Blogs.Add(new Blog
                        {
                            Url = "http://blogs.msdn.com/dotnet"
                        });
                        context1.SaveChanges();

                        using (var context2 = new BloggingContext(options))
                        {
                            context2.Database.UseTransaction(transaction.GetDbTransaction());

                            var blogs = context2.Blogs.OrderBy(b => b.Url).ToList();
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }

            #endregion
        }
    }
}

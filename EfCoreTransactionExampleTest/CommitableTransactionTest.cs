﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EfCoreTransactionExampleTest
{
    public class CommitableTransactionTest
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

            #region Transaction
            using (var transaction = new CommittableTransaction(
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var connection = new SqlConnection(connectionString);

                try
                {
                    var options = new DbContextOptionsBuilder<BloggingContext>()
                        .UseSqlServer(connection)
                        .Options;

                    using (var context = new BloggingContext(options))
                    {
                        context.Database.OpenConnection();
                        context.Database.EnlistTransaction(transaction);

                        // Run raw ADO.NET command in the transaction
                        var command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM dbo.Blogs";
                        command.ExecuteNonQuery();

                        // Run an EF Core command in the transaction
                        context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
                        context.SaveChanges();
                        context.Database.CloseConnection();
                    }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    // TODO: Handle failure
                }
            }
            #endregion
        }
    }
}

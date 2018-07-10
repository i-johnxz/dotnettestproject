using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTPCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Server=(localdb)\projects;Database=EFGetStarted.NewDb;Integrated Security=true;";

            var options = new DbContextOptionsBuilder<BankContext>()
                //.UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new BankContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            using (var context = new BankContext(options))
            {
                context.BankAccounts.Add(new BankAccount
                {
                    BankName = "china bank",
                    Number = "100001",
                    Owner = "zhangsan",
                    Swift = "basic"
                });
                context.BankAccounts.Add(new BankAccount
                {
                    BankName = "china bank",
                    Number = "100002",
                    Owner = "lisi",
                    Swift = "basic"
                });
                context.CreditCards.Add(new CreditCard
                {
                    CardType = 1,
                    ExpiryMonth = 12,
                    ExpiryYear = 2020,
                    Number = "100003",
                    Owner = "wangwu"
                });
                context.CreditCards.Add(new CreditCard
                {
                    CardType = 2,
                    ExpiryMonth = 12,
                    ExpiryYear = 2030,
                    Number = "100004",
                    Owner = "zhaoliu"
                });
                context.SaveChanges();
            }

            using (var context = new BankContext(options))
            {

                var billDetails = context.BillingDetails.ToList();
            }

            Console.WriteLine("Hello World!");
        }
    }
}

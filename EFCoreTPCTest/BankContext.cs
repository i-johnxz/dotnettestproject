using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTPCTest
{
    public class BankContext : DbContext
    {

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {

        }

        public DbSet<BillingDetail> BillingDetails { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().ToTable("BankAccounts");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");

            base.OnModelCreating(modelBuilder);
        }
    }
}

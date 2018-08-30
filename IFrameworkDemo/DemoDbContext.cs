using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.MessageStores.Relational;
using IFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace IFrameworkDemo
{
    public class DemoDbContext: MessageStore
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<BlogHistory> BlogHistorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void EnsureSeed()
        {
            Database.Migrate();
            
        }

    }



}

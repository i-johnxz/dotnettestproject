using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogsCore.Models;
using BlogsCore.Models.EntityTypeConfigurations;

namespace BlogsCore.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new BlogEntityTypeConfiguration());
            
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CarEntityTypeConfiguration());
            
            modelBuilder.ApplyConfiguration(new RecordOfSaleEntityTypeConfiguration());
            
            modelBuilder.ApplyConfiguration(new PostTagEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new BankAccountEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CreditCardEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<BlogsCore.Models.PostTag> PostTag { get; set; }

    }
}

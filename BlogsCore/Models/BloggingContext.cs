using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogsCore.Models;

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
            modelBuilder.Entity<Person>()
                        .Property(p => p.LastName)
                        .IsConcurrencyToken();
            

            modelBuilder.Entity<Blog>()
                        .Property(p => p.Timestamp)
                        .IsRowVersion();

            modelBuilder.Entity<Blog>()
                        .Property<DateTime>("LastUpdated");

            

            modelBuilder.Entity<Post>()
                        .HasOne(p => p.Blog)
                        .WithMany(s => s.Posts)
                        .HasForeignKey(s => s.BlogId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>().HasKey(c => new { c.State, c.LicensePlate });
            modelBuilder.Entity<RecordOfSale>()
                        .HasOne(s => s.Car)
                        .WithMany(c => c.SaleHistory)
                        .HasForeignKey(s => new {s.CarState, s.CarLicensePlate});

            modelBuilder.Entity<PostTag>()
                        .HasKey(t => new {t.PostId, t.TagId});

            modelBuilder.Entity<PostTag>()
                        .HasOne(pt => pt.Post)
                        .WithMany(p => p.PostTags)
                        .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.PostTags)
                        .HasForeignKey(pt => pt.TagId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<BlogsCore.Models.PostTag> PostTag { get; set; }

    }
}

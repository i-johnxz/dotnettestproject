using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTransactionExampleTest
{
    public class BloggingContext : DbContext
    {

        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<RssBlog> RssBlogs { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\projects;Database=EFGetStarted.NewDb;Integrated Security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

    }

    public class RssBlog : Blog
    {
        public string Rss { get; set; }
    }
}

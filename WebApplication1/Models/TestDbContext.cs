using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TestDbContext : IFramework.EntityFramework.MSDbContext
    {

        public TestDbContext()
            : base("TestDbContext")
        {
            
        }

        public DbSet<WeChatCorp> WeChatCorps { get; set; }

        public DbSet<BoundWeChatUser> BoundWeChatUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeChatCorp>().Property(c => c.Id).HasMaxLength(128);
            modelBuilder.Entity<WeChatCorp>().Property(c => c.Name).HasMaxLength(500);
            
            base.OnModelCreating(modelBuilder);
        }
    }


    public class BoundWeChatUser
    {
        public long Id { get; set; }

        public string CorpId { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string WeChatUserId { get; set; }

        public string WeChatNickName { get; set; }


        public DateTime? CreatedAt { get; set; }
    }

    public class WeChatCorp
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool AllowUserBind { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
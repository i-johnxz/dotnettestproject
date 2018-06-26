namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeChatCorp_AllowUserBind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeChatCorps", "AllowUserBind", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeChatCorps", "AllowUserBind");
        }
    }
}

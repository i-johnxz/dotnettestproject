namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoundWeChatUsers", "CorpId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoundWeChatUsers", "CorpId");
        }
    }
}

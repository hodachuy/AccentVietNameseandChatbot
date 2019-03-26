namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddU24 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards");
            DropIndex("dbo.QuickReplys", new[] { "CardID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.QuickReplys", "CardID");
            AddForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards", "ID");
        }
    }
}

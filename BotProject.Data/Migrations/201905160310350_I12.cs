namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class I12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "BotID", "dbo.Bots");
            DropIndex("dbo.Cards", new[] { "BotID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Cards", "BotID");
            AddForeignKey("dbo.Cards", "BotID", "dbo.Bots", "ID", cascadeDelete: true);
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateGC : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Cards", "BotID");
            AddForeignKey("dbo.Cards", "BotID", "dbo.Bots", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "BotID", "dbo.Bots");
            DropIndex("dbo.Cards", new[] { "BotID" });
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upImg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "CardID", "dbo.Cards");
            DropIndex("dbo.Images", new[] { "CardID" });
            AddColumn("dbo.Images", "BotID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "BotID");
            CreateIndex("dbo.Images", "CardID");
            AddForeignKey("dbo.Images", "CardID", "dbo.Cards", "ID", cascadeDelete: true);
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupCard : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Cards", "GroupCardID");
            AddForeignKey("dbo.Cards", "GroupCardID", "dbo.GroupCards", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "GroupCardID", "dbo.GroupCards");
            DropIndex("dbo.Cards", new[] { "GroupCardID" });
        }
    }
}

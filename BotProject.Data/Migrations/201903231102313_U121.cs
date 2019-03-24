namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U121 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Answers", name: "ContentCardID", newName: "CardID");
            RenameIndex(table: "dbo.Answers", name: "IX_ContentCardID", newName: "IX_CardID");
            AddColumn("dbo.Answers", "CardPayload", c => c.String());
            DropColumn("dbo.Answers", "TempSrai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "TempSrai", c => c.String());
            DropColumn("dbo.Answers", "CardPayload");
            RenameIndex(table: "dbo.Answers", name: "IX_CardID", newName: "IX_ContentCardID");
            RenameColumn(table: "dbo.Answers", name: "CardID", newName: "ContentCardID");
        }
    }
}

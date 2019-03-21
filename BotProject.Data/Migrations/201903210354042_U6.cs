namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ButtonLinks", "TempGnrItemID", c => c.Int());
            AddColumn("dbo.ButtonPostbacks", "TempGnrItemID", c => c.Int());
            AddColumn("dbo.TemplateGenericItems", "Title", c => c.String());
            DropColumn("dbo.ButtonLinks", "TempGnrID");
            DropColumn("dbo.ButtonPostbacks", "TempGnrID");
            DropColumn("dbo.TemplateGenericItems", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TemplateGenericItems", "Type", c => c.String());
            AddColumn("dbo.ButtonPostbacks", "TempGnrID", c => c.Int());
            AddColumn("dbo.ButtonLinks", "TempGnrID", c => c.Int());
            DropColumn("dbo.TemplateGenericItems", "Title");
            DropColumn("dbo.ButtonPostbacks", "TempGnrItemID");
            DropColumn("dbo.ButtonLinks", "TempGnrItemID");
        }
    }
}

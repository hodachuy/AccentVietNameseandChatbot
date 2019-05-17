namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ButtonLinks", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.ButtonPostbacks", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.QuickReplys", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.TemplateGenericGroups", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.TemplateGenericItems", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.TemplateTexts", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TemplateTexts", "Index");
            DropColumn("dbo.TemplateGenericItems", "Index");
            DropColumn("dbo.TemplateGenericGroups", "Index");
            DropColumn("dbo.QuickReplys", "Index");
            DropColumn("dbo.Images", "Index");
            DropColumn("dbo.ButtonPostbacks", "Index");
            DropColumn("dbo.ButtonLinks", "Index");
        }
    }
}

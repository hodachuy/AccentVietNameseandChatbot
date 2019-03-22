namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups");
            DropIndex("dbo.TemplateGenericItems", new[] { "TempGnrGroupID" });
            AddColumn("dbo.TemplateGenericItems", "TemplateGenericGroups_ID", c => c.Int());
            CreateIndex("dbo.TemplateGenericItems", "TemplateGenericGroups_ID");
            AddForeignKey("dbo.TemplateGenericItems", "TemplateGenericGroups_ID", "dbo.TemplateGenericGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemplateGenericItems", "TemplateGenericGroups_ID", "dbo.TemplateGenericGroups");
            DropIndex("dbo.TemplateGenericItems", new[] { "TemplateGenericGroups_ID" });
            DropColumn("dbo.TemplateGenericItems", "TemplateGenericGroups_ID");
            CreateIndex("dbo.TemplateGenericItems", "TempGnrGroupID");
            AddForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups", "ID");
        }
    }
}

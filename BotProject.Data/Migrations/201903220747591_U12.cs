namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TemplateGenericItems", "AttachmentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TemplateGenericItems", "AttachmentID");
        }
    }
}

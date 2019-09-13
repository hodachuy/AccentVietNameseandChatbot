namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardV5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "TemplateJsonFacebook", c => c.String());
            AddColumn("dbo.Cards", "TemplateJsonZalo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "TemplateJsonZalo");
            DropColumn("dbo.Cards", "TemplateJsonFacebook");
        }
    }
}

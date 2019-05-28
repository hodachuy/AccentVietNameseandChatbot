namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModulePayload : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Modules", "Title", c => c.String(maxLength: 150));
            AlterColumn("dbo.Modules", "Text", c => c.String(maxLength: 150));
            AlterColumn("dbo.Modules", "Key", c => c.String(maxLength: 150));
            AlterColumn("dbo.Modules", "Value", c => c.String(maxLength: 250));
            AlterColumn("dbo.Modules", "Payload", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Modules", "Payload", c => c.Int(nullable: false));
            AlterColumn("dbo.Modules", "Value", c => c.String());
            AlterColumn("dbo.Modules", "Key", c => c.String());
            AlterColumn("dbo.Modules", "Text", c => c.String());
            AlterColumn("dbo.Modules", "Title", c => c.String());
        }
    }
}

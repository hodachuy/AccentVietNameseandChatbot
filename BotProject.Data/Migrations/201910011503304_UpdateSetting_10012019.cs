namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSetting_10012019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "IsProactiveMessageZalo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "IsProactiveMessageFacebook", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "IsHaveMaintenance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "MessageMaintenance", c => c.String());
            DropColumn("dbo.Settings", "IsProactiveMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "IsProactiveMessage", c => c.Boolean(nullable: false));
            DropColumn("dbo.Settings", "MessageMaintenance");
            DropColumn("dbo.Settings", "IsHaveMaintenance");
            DropColumn("dbo.Settings", "IsProactiveMessageFacebook");
            DropColumn("dbo.Settings", "IsProactiveMessageZalo");
        }
    }
}

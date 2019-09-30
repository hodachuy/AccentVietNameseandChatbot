namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "IsProactiveMessage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "Timeout", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "ProactiveMessageText", c => c.String());
            AddColumn("dbo.Settings", "FacebookPageToken", c => c.String());
            AddColumn("dbo.Settings", "FacebookAppSecrect", c => c.String());
            AddColumn("dbo.Settings", "ZaloPageToken", c => c.String());
            AddColumn("dbo.Settings", "ZaloAppSecrect", c => c.String());
            AddColumn("dbo.Settings", "ZaloQRCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "ZaloQRCode");
            DropColumn("dbo.Settings", "ZaloAppSecrect");
            DropColumn("dbo.Settings", "ZaloPageToken");
            DropColumn("dbo.Settings", "FacebookAppSecrect");
            DropColumn("dbo.Settings", "FacebookPageToken");
            DropColumn("dbo.Settings", "ProactiveMessageText");
            DropColumn("dbo.Settings", "Timeout");
            DropColumn("dbo.Settings", "IsProactiveMessage");
        }
    }
}

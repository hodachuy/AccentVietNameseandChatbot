namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChatConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatSurveys", "IsShowName", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChatSurveys", "IsShowEmail", c => c.String());
            AddColumn("dbo.ChatSurveys", "IsShowPhone", c => c.String());
            AddColumn("dbo.ChatSurveys", "ChannelGroupID", c => c.Long(nullable: false));
            AddColumn("dbo.ChatWidgetCustomizations", "ChannelGroupID", c => c.Long(nullable: false));
            AddColumn("dbo.ChatWidgetLanguages", "ChannelGroupID", c => c.Long(nullable: false));
            AddColumn("dbo.Customers", "Name", c => c.String(maxLength: 200));
            DropColumn("dbo.ChatSurveys", "Name");
            DropColumn("dbo.ChatSurveys", "Email");
            DropColumn("dbo.ChatSurveys", "Phone");
            DropColumn("dbo.ChatSurveys", "CustomerID");
            DropColumn("dbo.ChatWidgetCustomizations", "BotID");
            DropColumn("dbo.ChatWidgetLanguages", "BotID");
            DropColumn("dbo.Customers", "FullName");
            DropColumn("dbo.Customers", "Avatar");
            DropColumn("dbo.Customers", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "Avatar", c => c.String());
            AddColumn("dbo.Customers", "FullName", c => c.String(maxLength: 200));
            AddColumn("dbo.ChatWidgetLanguages", "BotID", c => c.Int(nullable: false));
            AddColumn("dbo.ChatWidgetCustomizations", "BotID", c => c.Int(nullable: false));
            AddColumn("dbo.ChatSurveys", "CustomerID", c => c.String());
            AddColumn("dbo.ChatSurveys", "Phone", c => c.String());
            AddColumn("dbo.ChatSurveys", "Email", c => c.String());
            AddColumn("dbo.ChatSurveys", "Name", c => c.String());
            DropColumn("dbo.Customers", "Name");
            DropColumn("dbo.ChatWidgetLanguages", "ChannelGroupID");
            DropColumn("dbo.ChatWidgetCustomizations", "ChannelGroupID");
            DropColumn("dbo.ChatSurveys", "ChannelGroupID");
            DropColumn("dbo.ChatSurveys", "IsShowPhone");
            DropColumn("dbo.ChatSurveys", "IsShowEmail");
            DropColumn("dbo.ChatSurveys", "IsShowName");
        }
    }
}

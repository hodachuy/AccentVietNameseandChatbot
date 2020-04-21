namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChatWidget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatWidgetCustomizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MinimizedWindow = c.String(maxLength: 50),
                        MaximizeWindow = c.String(maxLength: 50),
                        ThemeColor = c.String(maxLength: 50),
                        Position = c.String(maxLength: 50),
                        UrlLogo = c.String(),
                        BotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChatWidgetLanguages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WelcomeMessage = c.String(),
                        WelcomeCardID = c.Int(),
                        TickedConfirmMessage = c.String(),
                        BotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatWidgetLanguages");
            DropTable("dbo.ChatWidgetCustomizations");
        }
    }
}

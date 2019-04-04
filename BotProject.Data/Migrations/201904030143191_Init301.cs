namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init301 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "TextIntroductory", c => c.String());
            AddColumn("dbo.Settings", "IsActiveIntroductory", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "IsActiveIntroductory");
            DropColumn("dbo.Settings", "TextIntroductory");
        }
    }
}

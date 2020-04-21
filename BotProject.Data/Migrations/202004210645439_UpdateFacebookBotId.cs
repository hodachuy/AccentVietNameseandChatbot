namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFacebookBotId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "BotID", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "BotID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationZaloUsers", "BotID");
            DropColumn("dbo.ApplicationFacebookUsers", "BotID");
        }
    }
}

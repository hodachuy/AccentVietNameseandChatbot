namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSystemConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemConfigs", "BotID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemConfigs", "BotID");
        }
    }
}

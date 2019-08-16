namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "StopWord", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "StopWord");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDateChannelGroup1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChannelGroups", "BotID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChannelGroups", "BotID");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBotCloneID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bots", "BotCloneParentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bots", "BotCloneParentID");
        }
    }
}

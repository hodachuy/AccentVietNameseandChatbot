namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Name");
        }
    }
}

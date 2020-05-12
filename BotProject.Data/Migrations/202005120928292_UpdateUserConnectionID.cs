namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserConnectionID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "ConnectionID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "ConnectionID");
        }
    }
}

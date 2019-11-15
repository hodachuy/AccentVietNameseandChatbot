namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFbUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "TimeStamp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationFacebookUsers", "TimeStamp");
        }
    }
}

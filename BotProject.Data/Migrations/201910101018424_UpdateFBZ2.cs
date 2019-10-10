namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFBZ2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.ApplicationZaloUsers", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationZaloUsers", "CreatedDate");
            DropColumn("dbo.ApplicationFacebookUsers", "CreatedDate");
        }
    }
}

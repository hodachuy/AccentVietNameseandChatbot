namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAppFacebookUserv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "UserName", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationFacebookUsers", "PhoneNumber");
            DropColumn("dbo.ApplicationFacebookUsers", "UserName");
        }
    }
}

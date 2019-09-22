namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppUserFacebook : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FacebookAppUsers", newName: "AppFacebookUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AppFacebookUsers", newName: "FacebookAppUsers");
        }
    }
}

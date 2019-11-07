namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFBzalo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "BranchOTP", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "BranchOTP", c => c.String());
            DropColumn("dbo.ApplicationFacebookUsers", "TimeOutOTP");
            DropColumn("dbo.ApplicationZaloUsers", "TimeOutOTP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationZaloUsers", "TimeOutOTP", c => c.DateTime());
            AddColumn("dbo.ApplicationFacebookUsers", "TimeOutOTP", c => c.DateTime());
            DropColumn("dbo.ApplicationZaloUsers", "BranchOTP");
            DropColumn("dbo.ApplicationFacebookUsers", "BranchOTP");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB_04112019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "TimeOutOTP", c => c.DateTime());
            AddColumn("dbo.ApplicationZaloUsers", "TimeOutOTP", c => c.DateTime());
            AddColumn("dbo.Settings", "TimeoutOTP", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "IsHaveTimeoutOTP", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "MessageTimeoutOTP", c => c.String());
            AddColumn("dbo.UserTelePhones", "BranchOTP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTelePhones", "BranchOTP");
            DropColumn("dbo.Settings", "MessageTimeoutOTP");
            DropColumn("dbo.Settings", "IsHaveTimeoutOTP");
            DropColumn("dbo.Settings", "TimeoutOTP");
            DropColumn("dbo.ApplicationZaloUsers", "TimeOutOTP");
            DropColumn("dbo.ApplicationFacebookUsers", "TimeOutOTP");
        }
    }
}

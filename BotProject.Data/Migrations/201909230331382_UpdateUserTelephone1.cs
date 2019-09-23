namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTelephone1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTelePhones", "Type", c => c.String());
            AddColumn("dbo.UserTelePhones", "SerialNumber", c => c.String());
            AddColumn("dbo.UserTelePhones", "NumberOrder", c => c.String());
            DropColumn("dbo.MdVouchers", "Type");
            DropColumn("dbo.MdVouchers", "SerialNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MdVouchers", "SerialNumber", c => c.String());
            AddColumn("dbo.MdVouchers", "Type", c => c.String());
            DropColumn("dbo.UserTelePhones", "NumberOrder");
            DropColumn("dbo.UserTelePhones", "SerialNumber");
            DropColumn("dbo.UserTelePhones", "Type");
        }
    }
}

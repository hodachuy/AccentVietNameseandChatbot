namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVoucherAndHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "Type", c => c.String());
            AddColumn("dbo.MdVouchers", "Type", c => c.String());
            AddColumn("dbo.MdVouchers", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdVouchers", "SerialNumber");
            DropColumn("dbo.MdVouchers", "Type");
            DropColumn("dbo.Histories", "Type");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTelePhones", "MdVoucherID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTelePhones", "MdVoucherID");
        }
    }
}

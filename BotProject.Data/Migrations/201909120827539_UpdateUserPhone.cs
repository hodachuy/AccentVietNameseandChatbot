namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserTelePhones", "MdVoucherID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTelePhones", "MdVoucherID", c => c.Int(nullable: false));
        }
    }
}

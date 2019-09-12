namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdVoucher3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MdVouchers", "ExpirationDate", c => c.DateTime());
            AlterColumn("dbo.MdVouchers", "StartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MdVouchers", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MdVouchers", "ExpirationDate", c => c.DateTime(nullable: false));
        }
    }
}

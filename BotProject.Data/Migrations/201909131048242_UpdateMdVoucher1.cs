namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdVoucher1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MdVouchers", "ModuleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MdVouchers", "ModuleID", c => c.Int(nullable: false));
        }
    }
}

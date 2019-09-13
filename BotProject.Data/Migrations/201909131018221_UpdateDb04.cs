namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ButtonModules", "MdVoucherID", c => c.Int());
            AddColumn("dbo.MdVouchers", "ButtonModuleID", c => c.Int());
            AddColumn("dbo.MdVouchers", "ModuleFollowCardID", c => c.Int());
            AddColumn("dbo.ModuleFollowCards", "MdVoucherID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModuleFollowCards", "MdVoucherID");
            DropColumn("dbo.MdVouchers", "ModuleFollowCardID");
            DropColumn("dbo.MdVouchers", "ButtonModuleID");
            DropColumn("dbo.ButtonModules", "MdVoucherID");
        }
    }
}

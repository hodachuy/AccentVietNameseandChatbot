namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdVoucher5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdVouchers", "TitlePayload", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdVouchers", "TitlePayload");
        }
    }
}

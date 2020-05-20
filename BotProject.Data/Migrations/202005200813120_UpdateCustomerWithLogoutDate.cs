namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerWithLogoutDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "LogoutDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "SendDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "SendDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "LogoutDate");
        }
    }
}

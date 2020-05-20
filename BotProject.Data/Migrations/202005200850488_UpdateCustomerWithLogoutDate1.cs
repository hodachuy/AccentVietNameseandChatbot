namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerWithLogoutDate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "LogoutDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "LogoutDate", c => c.DateTime(nullable: false));
        }
    }
}

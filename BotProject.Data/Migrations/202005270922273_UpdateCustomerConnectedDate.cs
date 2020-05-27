namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerConnectedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ConnectedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ConnectedDate");
        }
    }
}

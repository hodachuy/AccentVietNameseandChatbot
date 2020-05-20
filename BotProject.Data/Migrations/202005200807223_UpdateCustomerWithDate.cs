namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerWithDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SendDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "SendDate");
        }
    }
}

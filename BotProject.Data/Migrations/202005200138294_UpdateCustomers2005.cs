namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomers2005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Avatar");
        }
    }
}

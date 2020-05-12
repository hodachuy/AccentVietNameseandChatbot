namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomer12052020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ChannelGroupID", c => c.Long(nullable: false));
            DropColumn("dbo.Customers", "GroupChannelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "GroupChannelID", c => c.Long(nullable: false));
            DropColumn("dbo.Customers", "ChannelGroupID");
        }
    }
}

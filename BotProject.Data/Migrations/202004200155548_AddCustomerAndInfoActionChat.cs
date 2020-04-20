namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerAndInfoActionChat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 200),
                        Avatar = c.String(),
                        Email = c.String(maxLength: 200),
                        Gender = c.Boolean(nullable: false),
                        PhoneNumber = c.String(maxLength: 20),
                        ApplicationChannels = c.Int(nullable: false),
                        GroupChannelID = c.Long(nullable: false),
                        StatusChatValue = c.Int(nullable: false),
                        ActionChatValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IPAddress = c.String(maxLength: 15),
                        City = c.String(maxLength: 150),
                        Region = c.String(maxLength: 150),
                        Country = c.String(maxLength: 150),
                        Latitude = c.String(maxLength: 150),
                        Longtitude = c.String(maxLength: 150),
                        Timezone = c.String(maxLength: 150),
                        FullUserAgent = c.String(),
                        OS = c.String(maxLength: 50),
                        Browser = c.String(maxLength: 150),
                        IsMobile = c.Boolean(nullable: false),
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.StatusChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApplicationUsers", "StatusChatValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Devices", new[] { "CustomerID" });
            DropColumn("dbo.ApplicationUsers", "StatusChatValue");
            DropTable("dbo.StatusChats");
            DropTable("dbo.Devices");
            DropTable("dbo.Customers");
            DropTable("dbo.ActionChats");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "ActionChatValue");
            DropTable("dbo.ActionChats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ActionChats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Customers", "ActionChatValue", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "CreatedDate");
        }
    }
}

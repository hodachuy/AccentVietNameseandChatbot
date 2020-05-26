namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerTags",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.CustomerID, t.TagID })
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.CustomerTags", "CustomerID", "dbo.Customers");
            DropIndex("dbo.CustomerTags", new[] { "TagID" });
            DropIndex("dbo.CustomerTags", new[] { "CustomerID" });
            DropTable("dbo.Tags");
            DropTable("dbo.CustomerTags");
        }
    }
}

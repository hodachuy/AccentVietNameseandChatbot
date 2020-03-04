namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributePlatformUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttributePlatformUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        AttributeKey = c.String(),
                        AttributeValue = c.String(),
                        BotID = c.Int(nullable: false),
                        TypeDevice = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AttributePlatformUsers");
        }
    }
}

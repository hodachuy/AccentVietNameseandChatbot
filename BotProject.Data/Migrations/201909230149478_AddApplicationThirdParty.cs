namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationThirdParty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationThirdParties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PageID = c.String(),
                        Type = c.String(),
                        AccessToken = c.String(),
                        SecrectKey = c.String(),
                        BotID = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationThirdParties");
        }
    }
}

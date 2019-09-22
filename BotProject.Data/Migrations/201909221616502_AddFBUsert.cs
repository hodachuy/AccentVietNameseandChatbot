namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFBUsert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppFacebookUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        PredicateName = c.String(),
                        PredicateValue = c.String(),
                        PredicateIsCheck = c.Boolean(nullable: false),
                        StartedOn = c.DateTime(nullable: false),
                        MessageIsProactived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppFacebookUsers");
        }
    }
}

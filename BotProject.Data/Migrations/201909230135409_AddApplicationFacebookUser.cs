namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationFacebookUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationFacebookUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        PredicateName = c.String(),
                        PredicateValue = c.String(),
                        IsHavePredicate = c.Boolean(nullable: false),
                        IsProactiveMessage = c.Boolean(nullable: false),
                        StartedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationFacebookUsers");
        }
    }
}

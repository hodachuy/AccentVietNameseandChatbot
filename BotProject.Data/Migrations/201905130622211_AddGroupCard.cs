namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BotID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Cards", "GroupCardID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "GroupCardID");
            DropTable("dbo.GroupCards");
        }
    }
}

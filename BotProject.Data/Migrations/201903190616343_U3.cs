namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        CardID = c.Int(),
                        BotID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Cards", "Alias", c => c.String());
            DropColumn("dbo.Images", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Name", c => c.String());
            DropColumn("dbo.Cards", "Alias");
            DropTable("dbo.FileCards");
        }
    }
}

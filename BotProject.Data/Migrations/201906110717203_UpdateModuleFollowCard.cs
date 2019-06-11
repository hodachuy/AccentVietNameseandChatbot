namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModuleFollowCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModuleFollowCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartternText = c.String(),
                        Index = c.Int(nullable: false),
                        CardID = c.Int(),
                        BotID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ModuleFollowCards");
        }
    }
}

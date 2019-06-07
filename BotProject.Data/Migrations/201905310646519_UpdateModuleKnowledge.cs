namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModuleKnowledge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "CardPayloadID", c => c.Int());
            AddColumn("dbo.Modules", "CardID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Modules", "CardID");
            DropColumn("dbo.Modules", "CardPayloadID");
        }
    }
}

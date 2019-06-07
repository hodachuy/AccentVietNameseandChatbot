namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update685 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Modules", "CardPayloadID");
            DropColumn("dbo.Modules", "CardID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "CardID", c => c.Int());
            AddColumn("dbo.Modules", "CardPayloadID", c => c.Int());
        }
    }
}

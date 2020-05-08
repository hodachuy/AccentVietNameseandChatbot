namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBotActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bots", "IsActiveLiveChat", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bots", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bots", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bots", "IsActiveLiveChat");
        }
    }
}

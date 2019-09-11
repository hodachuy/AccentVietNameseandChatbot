namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "IsDelete");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bots", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bots", "IsActive");
        }
    }
}

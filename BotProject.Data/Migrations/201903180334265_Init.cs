namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bots", "Alias", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bots", "Alias");
        }
    }
}

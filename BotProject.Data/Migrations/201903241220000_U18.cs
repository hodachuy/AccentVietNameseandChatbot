namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BotQnAnswers", "Alias", c => c.String());
            AddColumn("dbo.BotQnAnswers", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BotQnAnswers", "Status");
            DropColumn("dbo.BotQnAnswers", "Alias");
        }
    }
}

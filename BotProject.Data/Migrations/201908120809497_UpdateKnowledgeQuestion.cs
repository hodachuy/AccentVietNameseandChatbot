namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKnowledgeQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Target", c => c.String());
            AddColumn("dbo.Questions", "IsSendAPI", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "IsSendAPI");
            DropColumn("dbo.Questions", "Target");
        }
    }
}

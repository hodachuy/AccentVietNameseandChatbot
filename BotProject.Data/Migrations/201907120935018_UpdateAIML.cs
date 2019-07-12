namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAIML : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AIMLs", "CardID", c => c.Int());
            AddColumn("dbo.AIMLs", "UserID", c => c.String());
            AlterColumn("dbo.AIMLs", "FormQnAnswerID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AIMLs", "FormQnAnswerID", c => c.Int(nullable: false));
            DropColumn("dbo.AIMLs", "UserID");
            DropColumn("dbo.AIMLs", "CardID");
        }
    }
}

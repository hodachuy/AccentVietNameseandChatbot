namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAll100 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BotQnAnswers", newName: "FormQuestionAnswers");
            DropForeignKey("dbo.AIMLs", "BotQnAnswerID", "dbo.BotQnAnswers");
            DropIndex("dbo.AIMLs", new[] { "BotQnAnswerID" });
            RenameColumn(table: "dbo.QuestionGroups", name: "BotQnAnswerID", newName: "FormQuestionAnswerID");
            RenameIndex(table: "dbo.QuestionGroups", name: "IX_BotQnAnswerID", newName: "IX_FormQuestionAnswerID");
            AddColumn("dbo.AIMLs", "FormQnAnswerID", c => c.Int(nullable: false));
            AddColumn("dbo.AIMLs", "BotID", c => c.Int(nullable: false));
            DropColumn("dbo.AIMLs", "BotQnAnswerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AIMLs", "BotQnAnswerID", c => c.Int(nullable: false));
            DropColumn("dbo.AIMLs", "BotID");
            DropColumn("dbo.AIMLs", "FormQnAnswerID");
            RenameIndex(table: "dbo.QuestionGroups", name: "IX_FormQuestionAnswerID", newName: "IX_BotQnAnswerID");
            RenameColumn(table: "dbo.QuestionGroups", name: "FormQuestionAnswerID", newName: "BotQnAnswerID");
            CreateIndex("dbo.AIMLs", "BotQnAnswerID");
            AddForeignKey("dbo.AIMLs", "BotQnAnswerID", "dbo.BotQnAnswers", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.FormQuestionAnswers", newName: "BotQnAnswers");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AIMLs", "BotID", "dbo.Bots");
            DropIndex("dbo.AIMLs", new[] { "BotID" });
            CreateTable(
                "dbo.BotQnAnswers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bots", t => t.BotID, cascadeDelete: true)
                .Index(t => t.BotID);
            
            AddColumn("dbo.AIMLs", "BotQnAnswerID", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionGroups", "BotQnAnswerID", c => c.Int(nullable: false));
            CreateIndex("dbo.AIMLs", "BotQnAnswerID");
            CreateIndex("dbo.QuestionGroups", "BotQnAnswerID");
            AddForeignKey("dbo.AIMLs", "BotQnAnswerID", "dbo.BotQnAnswers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.QuestionGroups", "BotQnAnswerID", "dbo.BotQnAnswers", "ID", cascadeDelete: true);
            DropColumn("dbo.AIMLs", "BotID");
            DropColumn("dbo.QuestionGroups", "BotID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionGroups", "BotID", c => c.Int(nullable: false));
            AddColumn("dbo.AIMLs", "BotID", c => c.Int(nullable: false));
            DropForeignKey("dbo.QuestionGroups", "BotQnAnswerID", "dbo.BotQnAnswers");
            DropForeignKey("dbo.AIMLs", "BotQnAnswerID", "dbo.BotQnAnswers");
            DropForeignKey("dbo.BotQnAnswers", "BotID", "dbo.Bots");
            DropIndex("dbo.QuestionGroups", new[] { "BotQnAnswerID" });
            DropIndex("dbo.BotQnAnswers", new[] { "BotID" });
            DropIndex("dbo.AIMLs", new[] { "BotQnAnswerID" });
            DropColumn("dbo.QuestionGroups", "BotQnAnswerID");
            DropColumn("dbo.AIMLs", "BotQnAnswerID");
            DropTable("dbo.BotQnAnswers");
            CreateIndex("dbo.AIMLs", "BotID");
            AddForeignKey("dbo.AIMLs", "BotID", "dbo.Bots", "ID", cascadeDelete: true);
        }
    }
}

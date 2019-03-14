namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "ContentCardID", "dbo.Cards");
            DropForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards");
            DropForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups");
            DropIndex("dbo.Answers", new[] { "ContentCardID" });
            DropIndex("dbo.QuickReplys", new[] { "CardID" });
            DropIndex("dbo.TemplateGenericItems", new[] { "TempGnrGroupID" });
            AlterColumn("dbo.Answers", "ContentCardID", c => c.Int());
            AlterColumn("dbo.Answers", "Index", c => c.Int());
            AlterColumn("dbo.QuestionGroups", "IsKeyword", c => c.Boolean());
            AlterColumn("dbo.ButtonLinks", "TempGnrID", c => c.Int());
            AlterColumn("dbo.ButtonLinks", "TempTxtID", c => c.Int());
            AlterColumn("dbo.ButtonLinks", "CardID", c => c.Int());
            AlterColumn("dbo.ButtonPostbacks", "TempGnrID", c => c.Int());
            AlterColumn("dbo.ButtonPostbacks", "TempTxtID", c => c.Int());
            AlterColumn("dbo.ButtonPostbacks", "CardPayloadID", c => c.Int());
            AlterColumn("dbo.ButtonPostbacks", "CardID", c => c.Int());
            AlterColumn("dbo.Questions", "IsThatStar", c => c.Boolean());
            AlterColumn("dbo.QuickReplys", "CardPayloadID", c => c.Int());
            AlterColumn("dbo.QuickReplys", "CardID", c => c.Int());
            AlterColumn("dbo.TemplateGenericItems", "TempGnrGroupID", c => c.Int());
            AlterColumn("dbo.TemplateTexts", "CardID", c => c.Int());
            CreateIndex("dbo.Answers", "ContentCardID");
            CreateIndex("dbo.QuickReplys", "CardID");
            CreateIndex("dbo.TemplateGenericItems", "TempGnrGroupID");
            AddForeignKey("dbo.Answers", "ContentCardID", "dbo.Cards", "ID");
            AddForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards", "ID");
            AddForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups");
            DropForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards");
            DropForeignKey("dbo.Answers", "ContentCardID", "dbo.Cards");
            DropIndex("dbo.TemplateGenericItems", new[] { "TempGnrGroupID" });
            DropIndex("dbo.QuickReplys", new[] { "CardID" });
            DropIndex("dbo.Answers", new[] { "ContentCardID" });
            AlterColumn("dbo.TemplateTexts", "CardID", c => c.Int(nullable: false));
            AlterColumn("dbo.TemplateGenericItems", "TempGnrGroupID", c => c.Int(nullable: false));
            AlterColumn("dbo.QuickReplys", "CardID", c => c.Int(nullable: false));
            AlterColumn("dbo.QuickReplys", "CardPayloadID", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "IsThatStar", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ButtonPostbacks", "CardID", c => c.Int(nullable: false));
            AlterColumn("dbo.ButtonPostbacks", "CardPayloadID", c => c.Int(nullable: false));
            AlterColumn("dbo.ButtonPostbacks", "TempTxtID", c => c.Int(nullable: false));
            AlterColumn("dbo.ButtonPostbacks", "TempGnrID", c => c.Int(nullable: false));
            AlterColumn("dbo.ButtonLinks", "CardID", c => c.Int(nullable: false));
            AlterColumn("dbo.ButtonLinks", "TempTxtID", c => c.Int(nullable: false));
            AlterColumn("dbo.ButtonLinks", "TempGnrID", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionGroups", "IsKeyword", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Answers", "Index", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "ContentCardID", c => c.Int(nullable: false));
            CreateIndex("dbo.TemplateGenericItems", "TempGnrGroupID");
            CreateIndex("dbo.QuickReplys", "CardID");
            CreateIndex("dbo.Answers", "ContentCardID");
            AddForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "ContentCardID", "dbo.Cards", "ID", cascadeDelete: true);
        }
    }
}

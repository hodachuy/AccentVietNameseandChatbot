namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdGetInfoPatientv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "OptionText", c => c.Int(nullable: false));
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "Payload", c => c.String());
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "Key", c => c.String());
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "Text");
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "PatternText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "PatternText", c => c.Int(nullable: false));
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "Text", c => c.String());
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "Key");
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "Payload");
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "OptionText");
        }
    }
}

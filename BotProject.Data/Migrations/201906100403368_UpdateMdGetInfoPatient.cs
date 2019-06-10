namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdGetInfoPatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "CardPayloadID", c => c.Int());
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "MessageEnd", c => c.String());
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "CardID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "CardID", c => c.Int());
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "MessageEnd");
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "CardPayloadID");
        }
    }
}

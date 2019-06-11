namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKnowledgeMdInfoPatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ModuleKnowledgeMedInfoPatients", "OptionText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ModuleKnowledgeMedInfoPatients", "OptionText", c => c.Int(nullable: false));
        }
    }
}

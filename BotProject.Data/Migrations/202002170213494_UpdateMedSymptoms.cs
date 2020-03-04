namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMedSymptoms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalSymptoms", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalSymptoms", "FileName");
        }
    }
}

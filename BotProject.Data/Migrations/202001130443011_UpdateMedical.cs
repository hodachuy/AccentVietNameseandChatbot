namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMedical : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalSymptoms", "ContentHTML", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalSymptoms", "ContentHTML");
        }
    }
}

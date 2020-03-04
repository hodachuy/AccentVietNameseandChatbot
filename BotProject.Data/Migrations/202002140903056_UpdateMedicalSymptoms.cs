namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMedicalSymptoms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalSymptoms", "Symptoms", c => c.String());
            AddColumn("dbo.MedicalSymptoms", "Predict", c => c.String());
            AddColumn("dbo.MedicalSymptoms", "Protect", c => c.String());
            AddColumn("dbo.MedicalSymptoms", "DoctorCanDo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalSymptoms", "DoctorCanDo");
            DropColumn("dbo.MedicalSymptoms", "Protect");
            DropColumn("dbo.MedicalSymptoms", "Predict");
            DropColumn("dbo.MedicalSymptoms", "Symptoms");
        }
    }
}

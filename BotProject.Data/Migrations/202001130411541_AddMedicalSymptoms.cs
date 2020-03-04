namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMedicalSymptoms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalSymptoms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Cause = c.String(),
                        Treament = c.String(),
                        Advice = c.String(),
                        BotID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MedicalSymptoms");
        }
    }
}

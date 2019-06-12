namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModuleFollowPatient090 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModuleFollowCards", "ModuleInfoPatientID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModuleFollowCards", "ModuleInfoPatientID");
        }
    }
}

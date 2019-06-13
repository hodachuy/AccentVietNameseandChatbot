namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModuleFlowCard9431 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModuleKnowledgeMedInfoPatients", "ModuleFollowCardID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModuleKnowledgeMedInfoPatients", "ModuleFollowCardID");
        }
    }
}

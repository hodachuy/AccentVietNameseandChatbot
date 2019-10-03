namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAIMLFILE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AIMLFiles", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AIMLFiles", "Status");
        }
    }
}

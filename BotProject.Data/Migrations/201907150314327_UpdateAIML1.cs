namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAIML1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AIMLs", newName: "AIMLFiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AIMLFiles", newName: "AIMLs");
        }
    }
}

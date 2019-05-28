namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModule : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Modules", newName: "ModuleCategories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ModuleCategories", newName: "Modules");
        }
    }
}

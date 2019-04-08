namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "IsMDSearch", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "IsMDSearch");
        }
    }
}

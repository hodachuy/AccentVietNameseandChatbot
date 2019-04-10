namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb70 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Settings", "IsActiveIntroductory", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Settings", "IsMDSearch", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settings", "IsMDSearch", c => c.Boolean());
            AlterColumn("dbo.Settings", "IsActiveIntroductory", c => c.Boolean());
        }
    }
}

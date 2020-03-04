namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSetting1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "PathCssCustom", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "PathCssCustom");
        }
    }
}

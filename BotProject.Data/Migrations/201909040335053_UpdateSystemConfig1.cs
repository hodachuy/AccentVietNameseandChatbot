namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSystemConfig1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SystemConfigs", "Code", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AlterColumn("dbo.SystemConfigs", "ValueString", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SystemConfigs", "ValueString", c => c.String(maxLength: 50));
            AlterColumn("dbo.SystemConfigs", "Code", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdSearch451 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MdSearchs", "CardPayloadID", c => c.Int(nullable: true));
            AlterColumn("dbo.MdSearchs", "ButtonModuleID", c => c.Int(nullable: true));
            AlterColumn("dbo.MdSearchs", "ModuleFollowCardID", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MdSearchs", "CardPayloadID", c => c.Int(nullable: false));
            AlterColumn("dbo.MdSearchs", "ButtonModuleID", c => c.Int(nullable: false));
            AlterColumn("dbo.MdSearchs", "ModuleFollowCardID", c => c.Int(nullable: false));
        }
    }
}

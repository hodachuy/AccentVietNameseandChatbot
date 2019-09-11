namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupCardIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupCards", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupCards", "IsDelete");
        }
    }
}

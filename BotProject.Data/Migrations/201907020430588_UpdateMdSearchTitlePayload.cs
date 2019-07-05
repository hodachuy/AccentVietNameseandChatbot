namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdSearchTitlePayload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdSearchs", "TitlePayload", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdSearchs", "TitlePayload");
        }
    }
}

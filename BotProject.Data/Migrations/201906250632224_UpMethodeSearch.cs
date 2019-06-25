namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpMethodeSearch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdSearchs", "MethodeAPI", c => c.String());
            DropColumn("dbo.MdSearchs", "MethodAPI");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MdSearchs", "MethodAPI", c => c.String());
            DropColumn("dbo.MdSearchs", "MethodeAPI");
        }
    }
}

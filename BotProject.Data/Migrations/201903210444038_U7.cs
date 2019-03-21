namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuickReplys", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuickReplys", "Title");
        }
    }
}

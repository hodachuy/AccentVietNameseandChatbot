namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionGroups", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionGroups", "CreatedDate");
        }
    }
}

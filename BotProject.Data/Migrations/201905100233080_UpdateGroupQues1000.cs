namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupQues1000 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionGroups", "BotID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionGroups", "BotID");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdQues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdQuestions", "BotID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdQuestions", "BotID");
        }
    }
}

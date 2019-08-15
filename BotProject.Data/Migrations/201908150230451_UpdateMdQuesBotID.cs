namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdQuesBotID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MdQuestions", "BotID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MdQuestions", "BotID", c => c.Int(nullable: false));
        }
    }
}

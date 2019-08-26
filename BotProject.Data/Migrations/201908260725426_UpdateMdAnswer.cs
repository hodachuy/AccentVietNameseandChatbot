namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdAnswers", "BotID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdAnswers", "BotID");
        }
    }
}

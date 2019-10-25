namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFormQuestionv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormQuestionAnswers", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormQuestionAnswers", "IsDelete");
        }
    }
}

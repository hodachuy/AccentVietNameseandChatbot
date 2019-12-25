namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardStep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "IsHaveCardStep", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "IsHaveCardStep", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "CardStepID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "CardStepID");
            DropColumn("dbo.ApplicationZaloUsers", "IsHaveCardStep");
            DropColumn("dbo.ApplicationFacebookUsers", "IsHaveCardStep");
        }
    }
}

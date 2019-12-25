namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFacebookZalo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "AttributeName", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "CardStepPattern", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "AttributeName", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "CardStepPattern", c => c.String());
            DropColumn("dbo.ApplicationFacebookUsers", "IsHaveCardStep");
            DropColumn("dbo.ApplicationZaloUsers", "IsHaveCardStep");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationZaloUsers", "IsHaveCardStep", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationFacebookUsers", "IsHaveCardStep", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicationZaloUsers", "CardStepPattern");
            DropColumn("dbo.ApplicationZaloUsers", "AttributeName");
            DropColumn("dbo.ApplicationFacebookUsers", "CardStepPattern");
            DropColumn("dbo.ApplicationFacebookUsers", "AttributeName");
        }
    }
}

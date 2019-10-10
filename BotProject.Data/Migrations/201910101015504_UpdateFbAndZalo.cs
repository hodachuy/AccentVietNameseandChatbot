namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFbAndZalo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "IsHaveCardCondition", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationFacebookUsers", "CardConditionPattern", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "IsHaveCardCondition", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "CardConditionPattern", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationZaloUsers", "CardConditionPattern");
            DropColumn("dbo.ApplicationZaloUsers", "IsHaveCardCondition");
            DropColumn("dbo.ApplicationFacebookUsers", "CardConditionPattern");
            DropColumn("dbo.ApplicationFacebookUsers", "IsHaveCardCondition");
        }
    }
}

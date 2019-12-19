namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFBookUserAndCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "IsConditionWithAreaButton", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationFacebookUsers", "CardConditionAreaButtonPattern", c => c.String());
            AddColumn("dbo.Cards", "IsConditionWithAreaButton", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "IsConditionWithAreaButton");
            DropColumn("dbo.ApplicationFacebookUsers", "CardConditionAreaButtonPattern");
            DropColumn("dbo.ApplicationFacebookUsers", "IsConditionWithAreaButton");
        }
    }
}

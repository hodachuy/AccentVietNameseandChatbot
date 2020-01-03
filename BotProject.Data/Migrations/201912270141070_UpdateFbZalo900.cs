namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFbZalo900 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "IsConditionWithInputText", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationFacebookUsers", "CardConditionWithInputTextPattern", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "IsConditionWithInputText", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "CardConditionWithInputTextPattern", c => c.String());
            AddColumn("dbo.Cards", "IsConditionWithInputText", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "IsConditionWithInputText");
            DropColumn("dbo.ApplicationZaloUsers", "CardConditionWithInputTextPattern");
            DropColumn("dbo.ApplicationZaloUsers", "IsConditionWithInputText");
            DropColumn("dbo.ApplicationFacebookUsers", "CardConditionWithInputTextPattern");
            DropColumn("dbo.ApplicationFacebookUsers", "IsConditionWithInputText");
        }
    }
}

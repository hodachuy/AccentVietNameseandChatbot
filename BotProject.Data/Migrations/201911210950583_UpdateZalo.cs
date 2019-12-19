namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateZalo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationZaloUsers", "IsConditionWithAreaButton", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "CardConditionAreaButtonPattern", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationZaloUsers", "CardConditionAreaButtonPattern");
            DropColumn("dbo.ApplicationZaloUsers", "IsConditionWithAreaButton");
        }
    }
}

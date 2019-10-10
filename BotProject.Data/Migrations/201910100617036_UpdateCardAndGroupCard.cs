namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardAndGroupCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "IsHaveCondition", c => c.Boolean(nullable: false));
            AddColumn("dbo.GroupCards", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupCards", "Status");
            DropColumn("dbo.Cards", "IsHaveCondition");
            DropColumn("dbo.Cards", "Status");
        }
    }
}

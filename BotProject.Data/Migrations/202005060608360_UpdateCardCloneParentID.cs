namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardCloneParentID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "CardCloneParentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "CardCloneParentID");
        }
    }
}

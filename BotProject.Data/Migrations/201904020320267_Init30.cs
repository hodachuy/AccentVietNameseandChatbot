namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "BotID", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "UserID", c => c.String());
            AlterColumn("dbo.Settings", "CardID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settings", "CardID", c => c.Long());
            DropColumn("dbo.Settings", "UserID");
            DropColumn("dbo.Settings", "BotID");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ContentText", c => c.String());
            AlterColumn("dbo.Questions", "Index", c => c.Int());
            DropColumn("dbo.Questions", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Content", c => c.String());
            AlterColumn("dbo.Questions", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "ContentText");
        }
    }
}

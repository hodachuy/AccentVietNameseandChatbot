namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupCard1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupCards", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupCards", "Index");
        }
    }
}

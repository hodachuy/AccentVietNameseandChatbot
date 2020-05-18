namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateThread2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Threads", "Index");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Threads", "Index", c => c.Long(nullable: false));
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateThreads : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Threads", "Index", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Threads", "Index");
        }
    }
}

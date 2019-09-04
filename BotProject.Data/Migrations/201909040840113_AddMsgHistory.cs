namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMsgHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "MessageHistory", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Histories", "MessageHistory");
        }
    }
}

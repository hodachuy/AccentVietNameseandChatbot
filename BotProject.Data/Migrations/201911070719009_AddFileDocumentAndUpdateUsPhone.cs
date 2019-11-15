namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileDocumentAndUpdateUsPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTelePhones", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTelePhones", "IsDelete");
        }
    }
}

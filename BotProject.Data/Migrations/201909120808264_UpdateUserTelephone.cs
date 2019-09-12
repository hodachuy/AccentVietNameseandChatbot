namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTelephone : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserTelePhones");
            AddColumn("dbo.UserTelePhones", "TelephoneNumber", c => c.String());
            AlterColumn("dbo.UserTelePhones", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserTelePhones", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserTelePhones");
            AlterColumn("dbo.UserTelePhones", "ID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserTelePhones", "TelephoneNumber");
            AddPrimaryKey("dbo.UserTelePhones", "ID");
        }
    }
}

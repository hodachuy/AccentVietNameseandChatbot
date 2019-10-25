namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTelephone2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTelePhones", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTelePhones", "CreatedDate");
        }
    }
}

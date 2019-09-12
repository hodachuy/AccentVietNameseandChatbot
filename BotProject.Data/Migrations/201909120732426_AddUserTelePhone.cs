namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTelePhone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTelePhones",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        TypeService = c.String(),
                        Code = c.String(),
                        NumberReceive = c.Int(nullable: false),
                        IsReceive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTelePhones");
        }
    }
}

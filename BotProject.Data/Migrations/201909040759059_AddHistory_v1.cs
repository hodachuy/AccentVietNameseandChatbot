namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHistory_v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Histories",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   UserName = c.String(),
                   UserSay = c.String(),
                   BotHandle = c.String(),
                   BotUnderStands = c.String(),
                   CreateDate = c.DateTime(nullable:true),                
                   BotID = c.Int(nullable: true),
               })
               .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Histories");
        }
    }
}

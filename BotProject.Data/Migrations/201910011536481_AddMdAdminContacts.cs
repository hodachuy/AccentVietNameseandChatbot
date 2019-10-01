namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMdAdminContacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdAdminContacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TitlePayload = c.String(),
                        Payload = c.String(),
                        CardPayloadID = c.Int(),
                        DictionaryKey = c.String(),
                        DictionaryValue = c.String(),
                        MessageStart = c.String(),
                        MessageError = c.String(),
                        MessageEnd = c.String(),
                        ModuleID = c.Int(nullable: false),
                        BotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MdAdminContacts");
        }
    }
}

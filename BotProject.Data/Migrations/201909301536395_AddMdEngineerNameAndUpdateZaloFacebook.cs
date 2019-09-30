namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMdEngineerNameAndUpdateZaloFacebook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdEngineerNames",
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
            
            AddColumn("dbo.ApplicationFacebookUsers", "EngineerName", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "TimeOut", c => c.DateTime());
            AddColumn("dbo.ApplicationZaloUsers", "EngineerName", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "TimeOut", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationZaloUsers", "TimeOut");
            DropColumn("dbo.ApplicationZaloUsers", "EngineerName");
            DropColumn("dbo.ApplicationFacebookUsers", "TimeOut");
            DropColumn("dbo.ApplicationFacebookUsers", "EngineerName");
            DropTable("dbo.MdEngineerNames");
        }
    }
}

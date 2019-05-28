namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMDPhoneEmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdEmails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
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
            
            CreateTable(
                "dbo.MdPhones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
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
            
            AddColumn("dbo.ButtonModules", "ModuleID", c => c.Int());
            AddColumn("dbo.ButtonModules", "ModuleKnowledgeID", c => c.Int());
            AddColumn("dbo.ButtonModules", "ModuleType", c => c.String());
            AddColumn("dbo.Modules", "Type", c => c.String());
            AlterColumn("dbo.Modules", "Payload", c => c.String());
            DropColumn("dbo.ButtonModules", "DictionaryKey");
            DropColumn("dbo.ButtonModules", "DictionaryValue");
            DropColumn("dbo.ButtonModules", "CardPayloadID");
            DropColumn("dbo.Modules", "Key");
            DropColumn("dbo.Modules", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "Value", c => c.String(maxLength: 250));
            AddColumn("dbo.Modules", "Key", c => c.String(maxLength: 150));
            AddColumn("dbo.ButtonModules", "CardPayloadID", c => c.Int());
            AddColumn("dbo.ButtonModules", "DictionaryValue", c => c.String());
            AddColumn("dbo.ButtonModules", "DictionaryKey", c => c.String());
            AlterColumn("dbo.Modules", "Payload", c => c.String(maxLength: 150));
            DropColumn("dbo.Modules", "Type");
            DropColumn("dbo.ButtonModules", "ModuleType");
            DropColumn("dbo.ButtonModules", "ModuleKnowledgeID");
            DropColumn("dbo.ButtonModules", "ModuleID");
            DropTable("dbo.MdPhones");
            DropTable("dbo.MdEmails");
        }
    }
}

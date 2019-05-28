namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateButton : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ButtonChecks", "DictionaryKey", c => c.String());
            AddColumn("dbo.ButtonChecks", "DictionaryValue", c => c.String());
            AddColumn("dbo.ButtonModules", "DictionaryKey", c => c.String());
            AddColumn("dbo.ButtonModules", "DictionaryValue", c => c.String());
            AddColumn("dbo.ButtonPostbacks", "DictionaryKey", c => c.String());
            AddColumn("dbo.ButtonPostbacks", "DictionaryValue", c => c.String());
            DropColumn("dbo.ButtonChecks", "KeyDictionary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ButtonChecks", "KeyDictionary", c => c.String());
            DropColumn("dbo.ButtonPostbacks", "DictionaryValue");
            DropColumn("dbo.ButtonPostbacks", "DictionaryKey");
            DropColumn("dbo.ButtonModules", "DictionaryValue");
            DropColumn("dbo.ButtonModules", "DictionaryKey");
            DropColumn("dbo.ButtonChecks", "DictionaryValue");
            DropColumn("dbo.ButtonChecks", "DictionaryKey");
        }
    }
}

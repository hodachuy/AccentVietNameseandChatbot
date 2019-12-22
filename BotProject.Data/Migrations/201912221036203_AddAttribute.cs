namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "AttributeSystemName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "AttributeSystemName");
        }
    }
}

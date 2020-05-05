namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiBot_04052020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bots", "IsTemplate", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bots", "ImageTemplate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bots", "ImageTemplate");
            DropColumn("dbo.Bots", "IsTemplate");
        }
    }
}

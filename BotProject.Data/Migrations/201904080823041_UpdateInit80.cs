namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInit80 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "FormName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "FormName");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class U21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CodeSymbol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "CodeSymbol");
        }
    }
}

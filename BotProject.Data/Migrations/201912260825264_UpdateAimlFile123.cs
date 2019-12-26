namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAimlFile123 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AIMLFiles", "Name", c => c.String());
            AlterColumn("dbo.AIMLFiles", "Src", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AIMLFiles", "Src", c => c.String(maxLength: 256));
            AlterColumn("dbo.AIMLFiles", "Name", c => c.String(maxLength: 50));
        }
    }
}

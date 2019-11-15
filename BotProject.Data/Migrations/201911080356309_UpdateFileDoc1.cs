namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFileDoc1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileDocuments", "TokenZalo", c => c.String());
            AddColumn("dbo.FileDocuments", "TokenFacebook", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileDocuments", "TokenFacebook");
            DropColumn("dbo.FileDocuments", "TokenZalo");
        }
    }
}

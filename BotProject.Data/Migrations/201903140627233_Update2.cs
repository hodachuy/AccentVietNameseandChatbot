namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bots", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Bots", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Bots", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Bots", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Bots", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Bots", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Bots", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bots", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bots", "Status");
            DropColumn("dbo.Bots", "MetaDescription");
            DropColumn("dbo.Bots", "MetaKeyword");
            DropColumn("dbo.Bots", "UpdatedBy");
            DropColumn("dbo.Bots", "UpdatedDate");
            DropColumn("dbo.Bots", "CreatedBy");
        }
    }
}

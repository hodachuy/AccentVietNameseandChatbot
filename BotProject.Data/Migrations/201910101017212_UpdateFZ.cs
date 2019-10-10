namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFZ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationFacebookUsers", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationZaloUsers", "Gender");
            DropColumn("dbo.ApplicationZaloUsers", "Age");
            DropColumn("dbo.ApplicationFacebookUsers", "Gender");
            DropColumn("dbo.ApplicationFacebookUsers", "Age");
        }
    }
}

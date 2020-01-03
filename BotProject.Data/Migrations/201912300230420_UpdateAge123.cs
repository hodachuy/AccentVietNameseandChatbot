namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAge123 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationFacebookUsers", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicationFacebookUsers", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ApplicationZaloUsers", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicationZaloUsers", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationZaloUsers", "Gender", c => c.String());
            AlterColumn("dbo.ApplicationZaloUsers", "Age", c => c.String());
            AlterColumn("dbo.ApplicationFacebookUsers", "Gender", c => c.String());
            AlterColumn("dbo.ApplicationFacebookUsers", "Age", c => c.String());
        }
    }
}

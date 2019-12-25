namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFacebookAndZalo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationFacebookUsers", "FirstName", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "LastName", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "Location", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "AvatarPicture", c => c.String());
            AddColumn("dbo.ApplicationFacebookUsers", "IsHaveSetAttributeSystem", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationZaloUsers", "FirstName", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "LastName", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "Location", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "AvatarPicture", c => c.String());
            AddColumn("dbo.ApplicationZaloUsers", "IsHaveSetAttributeSystem", c => c.Boolean(nullable: false));
            AddColumn("dbo.AttributeSystems", "IsDefaultSystem", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ApplicationFacebookUsers", "Age", c => c.String());
            AlterColumn("dbo.ApplicationFacebookUsers", "Gender", c => c.String());
            AlterColumn("dbo.ApplicationZaloUsers", "Age", c => c.String());
            AlterColumn("dbo.ApplicationZaloUsers", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationZaloUsers", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ApplicationZaloUsers", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicationFacebookUsers", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ApplicationFacebookUsers", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.AttributeSystems", "IsDefaultSystem");
            DropColumn("dbo.ApplicationZaloUsers", "IsHaveSetAttributeSystem");
            DropColumn("dbo.ApplicationZaloUsers", "AvatarPicture");
            DropColumn("dbo.ApplicationZaloUsers", "Location");
            DropColumn("dbo.ApplicationZaloUsers", "LastName");
            DropColumn("dbo.ApplicationZaloUsers", "FirstName");
            DropColumn("dbo.ApplicationFacebookUsers", "IsHaveSetAttributeSystem");
            DropColumn("dbo.ApplicationFacebookUsers", "AvatarPicture");
            DropColumn("dbo.ApplicationFacebookUsers", "Location");
            DropColumn("dbo.ApplicationFacebookUsers", "LastName");
            DropColumn("dbo.ApplicationFacebookUsers", "FirstName");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdAdminContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdAdminContacts", "MessageStart1", c => c.String());
            AddColumn("dbo.MdAdminContacts", "MessageStart2", c => c.String());
            AddColumn("dbo.MdAdminContacts", "MessageStart3", c => c.String());
            DropColumn("dbo.MdAdminContacts", "MessageStart");
            DropColumn("dbo.MdAdminContacts", "MessageError");
            DropColumn("dbo.MdAdminContacts", "MessageEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MdAdminContacts", "MessageEnd", c => c.String());
            AddColumn("dbo.MdAdminContacts", "MessageError", c => c.String());
            AddColumn("dbo.MdAdminContacts", "MessageStart", c => c.String());
            DropColumn("dbo.MdAdminContacts", "MessageStart3");
            DropColumn("dbo.MdAdminContacts", "MessageStart2");
            DropColumn("dbo.MdAdminContacts", "MessageStart1");
        }
    }
}

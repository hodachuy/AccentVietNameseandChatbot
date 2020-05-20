namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateThreadParticipant2005 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ThreadParticipants");
            AlterColumn("dbo.ThreadParticipants", "CustomerID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ThreadParticipants", new[] { "ThreadID", "ChannelGroupID" });
            CreateIndex("dbo.ThreadParticipants", "CustomerID");
            AddForeignKey("dbo.ThreadParticipants", "CustomerID", "dbo.Customers", "ID");
            DropColumn("dbo.ThreadParticipants", "ID");
            DropColumn("dbo.ThreadParticipants", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThreadParticipants", "UserID", c => c.String());
            AddColumn("dbo.ThreadParticipants", "ID", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.ThreadParticipants", "CustomerID", "dbo.Customers");
            DropIndex("dbo.ThreadParticipants", new[] { "CustomerID" });
            DropPrimaryKey("dbo.ThreadParticipants");
            AlterColumn("dbo.ThreadParticipants", "CustomerID", c => c.String());
            AddPrimaryKey("dbo.ThreadParticipants", "ID");
        }
    }
}

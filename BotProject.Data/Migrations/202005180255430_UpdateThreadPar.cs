namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateThreadPar : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ThreadParticipants");
            AddColumn("dbo.ThreadParticipants", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ThreadParticipants", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ThreadParticipants");
            DropColumn("dbo.ThreadParticipants", "ID");
            AddPrimaryKey("dbo.ThreadParticipants", new[] { "ThreadID", "ChannelGroupID" });
        }
    }
}

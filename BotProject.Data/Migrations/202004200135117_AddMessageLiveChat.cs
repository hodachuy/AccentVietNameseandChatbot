namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageLiveChat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        GroupChannelID = c.Long(nullable: false),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GroupChannels", t => t.GroupChannelID, cascadeDelete: true)
                .Index(t => t.GroupChannelID);
            
            CreateTable(
                "dbo.GroupChannels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messsages",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ThreadID = c.Long(nullable: false),
                        GroupChannelID = c.Long(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Body = c.String(),
                        SenderID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThreadParticipants",
                c => new
                    {
                        ThreadID = c.Long(nullable: false),
                        GroupChannelID = c.Long(nullable: false),
                        UserID = c.String(),
                        CustomerID = c.String(),
                    })
                .PrimaryKey(t => new { t.ThreadID, t.GroupChannelID })
                .ForeignKey("dbo.GroupChannels", t => t.GroupChannelID, cascadeDelete: true)
                .ForeignKey("dbo.Threads", t => t.ThreadID, cascadeDelete: true)
                .Index(t => t.ThreadID)
                .Index(t => t.GroupChannelID);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThreadParticipants", "ThreadID", "dbo.Threads");
            DropForeignKey("dbo.ThreadParticipants", "GroupChannelID", "dbo.GroupChannels");
            DropForeignKey("dbo.Channels", "GroupChannelID", "dbo.GroupChannels");
            DropIndex("dbo.ThreadParticipants", new[] { "GroupChannelID" });
            DropIndex("dbo.ThreadParticipants", new[] { "ThreadID" });
            DropIndex("dbo.Channels", new[] { "GroupChannelID" });
            DropTable("dbo.Threads");
            DropTable("dbo.ThreadParticipants");
            DropTable("dbo.Messsages");
            DropTable("dbo.GroupChannels");
            DropTable("dbo.Channels");
        }
    }
}

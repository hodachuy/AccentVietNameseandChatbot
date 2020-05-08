namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChannelGroup : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupChannels", newName: "ChannelGroups");
            RenameColumn(table: "dbo.ThreadParticipants", name: "GroupChannelID", newName: "ChannelGroupID");
            RenameColumn(table: "dbo.Channels", name: "GroupChannelID", newName: "ChannelGroupID");
            RenameIndex(table: "dbo.Channels", name: "IX_GroupChannelID", newName: "IX_ChannelGroupID");
            RenameIndex(table: "dbo.ThreadParticipants", name: "IX_GroupChannelID", newName: "IX_ChannelGroupID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ThreadParticipants", name: "IX_ChannelGroupID", newName: "IX_GroupChannelID");
            RenameIndex(table: "dbo.Channels", name: "IX_ChannelGroupID", newName: "IX_GroupChannelID");
            RenameColumn(table: "dbo.Channels", name: "ChannelGroupID", newName: "GroupChannelID");
            RenameColumn(table: "dbo.ThreadParticipants", name: "ChannelGroupID", newName: "GroupChannelID");
            RenameTable(name: "dbo.ChannelGroups", newName: "GroupChannels");
        }
    }
}

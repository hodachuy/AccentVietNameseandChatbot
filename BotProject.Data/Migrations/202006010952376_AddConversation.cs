namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConversation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ThreadID = c.Long(nullable: false),
                        ChannelGroupID = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Messsages", "ConversationID", c => c.Long(nullable: false));
            AddColumn("dbo.Messsages", "AgentID", c => c.String());
            AddColumn("dbo.Messsages", "CustomerID", c => c.String());
            AddColumn("dbo.Messsages", "IsBotChat", c => c.Boolean(nullable: false));
            DropColumn("dbo.Messsages", "ThreadID");
            DropColumn("dbo.Messsages", "SenderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messsages", "SenderID", c => c.String());
            AddColumn("dbo.Messsages", "ThreadID", c => c.Long(nullable: false));
            DropColumn("dbo.Messsages", "IsBotChat");
            DropColumn("dbo.Messsages", "CustomerID");
            DropColumn("dbo.Messsages", "AgentID");
            DropColumn("dbo.Messsages", "ConversationID");
            DropTable("dbo.Conversations");
        }
    }
}

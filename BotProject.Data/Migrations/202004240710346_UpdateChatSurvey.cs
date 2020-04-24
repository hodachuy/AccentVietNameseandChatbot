namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChatSurvey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatSurveys",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        CustomerID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Customers", "ConnectionID", c => c.String());
            AddColumn("dbo.ThreadParticipants", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Messsages", "GroupChannelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messsages", "GroupChannelID", c => c.Long(nullable: false));
            DropColumn("dbo.ThreadParticipants", "CreatedDate");
            DropColumn("dbo.Customers", "ConnectionID");
            DropTable("dbo.ChatSurveys");
        }
    }
}

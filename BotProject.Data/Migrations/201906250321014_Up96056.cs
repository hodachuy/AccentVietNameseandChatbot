namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Up96056 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.MdSearchs",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   Title = c.String(),
                   Payload = c.String(),
                   CardPayloadID = c.Int(nullable: false),
                   UrlAPI = c.String(),
                   KeyAPI = c.String(),
                   MethodAPI = c.String(),
                   ParamAPI = c.String(),
                   MessageEnd = c.String(),
                   MessageStart = c.String(),
                   MessageError = c.String(),
                   ButtonModuleID = c.Int(nullable: false),
                   ModuleFollowCardID = c.Int(nullable: false),
                   BotID = c.Int(),
               })
               .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.MdSearchs");

        }
    }
}

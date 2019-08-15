namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdAreas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdAreas", "BotID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdAreas", "BotID");
        }
    }
}

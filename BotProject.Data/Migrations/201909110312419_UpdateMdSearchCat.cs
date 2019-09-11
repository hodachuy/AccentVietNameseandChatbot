namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMdSearchCat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdSearchCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.MdSearchs", "MdSearchCategoryID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdSearchs", "MdSearchCategoryID");
            DropTable("dbo.MdSearchCategories");
        }
    }
}

namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AIMLs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BotID = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Src = c.String(maxLength: 256),
                        Extension = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bots", t => t.BotID, cascadeDelete: true)
                .Index(t => t.BotID);
            
            CreateTable(
                "dbo.Bots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        UserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        BirthDay = c.DateTime(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ApplicationRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContentText = c.String(),
                        ContentCardID = c.Int(),
                        QuestionGroupID = c.Int(nullable: false),
                        TempSrai = c.String(),
                        Index = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cards", t => t.ContentCardID)
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroupID, cascadeDelete: true)
                .Index(t => t.ContentCardID)
                .Index(t => t.QuestionGroupID);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        PatternText = c.String(),
                        TemplateAIML = c.String(),
                        TemplateHTML = c.String(),
                        TemplateJSON = c.String(),
                        BotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        IsKeyword = c.Boolean(),
                        BotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ButtonLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Title = c.String(maxLength: 50),
                        Url = c.String(maxLength: 256),
                        SizeHeight = c.String(),
                        TempGnrID = c.Int(),
                        TempTxtID = c.Int(),
                        CardID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ButtonPostbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Payload = c.String(),
                        Title = c.String(),
                        TempGnrID = c.Int(),
                        TempTxtID = c.Int(),
                        CardPayloadID = c.Int(),
                        CardID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        CardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cards", t => t.CardID, cascadeDelete: true)
                .Index(t => t.CardID);
            
            CreateTable(
                "dbo.Notifies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 256),
                        SenderID = c.Int(),
                        RecipientID = c.Int(),
                        IsRead = c.Boolean(),
                        TableName = c.String(maxLength: 50),
                        TableID = c.Int(),
                        CreatedDate = c.DateTime(),
                        URL = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Index = c.Int(nullable: false),
                        IsThatStar = c.Boolean(),
                        TempSrai = c.String(),
                        QuestionGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroupID, cascadeDelete: true)
                .Index(t => t.QuestionGroupID);
            
            CreateTable(
                "dbo.QuickReplys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContentType = c.String(),
                        Payload = c.String(),
                        CardPayloadID = c.Int(),
                        CardID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cards", t => t.CardID)
                .Index(t => t.CardID);
            
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        ValueString = c.String(maxLength: 50),
                        ValueInt = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TemplateGenericGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        CardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TemplateGenericItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        SubTitle = c.String(),
                        Url = c.String(),
                        Image = c.String(),
                        TempGnrGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TemplateGenericGroups", t => t.TempGnrGroupID)
                .Index(t => t.TempGnrGroupID);
            
            CreateTable(
                "dbo.TemplateTexts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Text = c.String(),
                        CardID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VisitorStatistics",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        VisitedDate = c.DateTime(nullable: false),
                        IPAddress = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemplateGenericItems", "TempGnrGroupID", "dbo.TemplateGenericGroups");
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.QuickReplys", "CardID", "dbo.Cards");
            DropForeignKey("dbo.Questions", "QuestionGroupID", "dbo.QuestionGroups");
            DropForeignKey("dbo.Images", "CardID", "dbo.Cards");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.Answers", "QuestionGroupID", "dbo.QuestionGroups");
            DropForeignKey("dbo.Answers", "ContentCardID", "dbo.Cards");
            DropForeignKey("dbo.AIMLs", "BotID", "dbo.Bots");
            DropForeignKey("dbo.Bots", "UserID", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.TemplateGenericItems", new[] { "TempGnrGroupID" });
            DropIndex("dbo.QuickReplys", new[] { "CardID" });
            DropIndex("dbo.Questions", new[] { "QuestionGroupID" });
            DropIndex("dbo.Images", new[] { "CardID" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.Answers", new[] { "QuestionGroupID" });
            DropIndex("dbo.Answers", new[] { "ContentCardID" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Bots", new[] { "UserID" });
            DropIndex("dbo.AIMLs", new[] { "BotID" });
            DropTable("dbo.VisitorStatistics");
            DropTable("dbo.TemplateTexts");
            DropTable("dbo.TemplateGenericItems");
            DropTable("dbo.TemplateGenericGroups");
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.QuickReplys");
            DropTable("dbo.Questions");
            DropTable("dbo.Notifies");
            DropTable("dbo.Images");
            DropTable("dbo.Errors");
            DropTable("dbo.ButtonPostbacks");
            DropTable("dbo.ButtonLinks");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationRoles");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationGroups");
            DropTable("dbo.QuestionGroups");
            DropTable("dbo.Cards");
            DropTable("dbo.Answers");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Bots");
            DropTable("dbo.AIMLs");
        }
    }
}

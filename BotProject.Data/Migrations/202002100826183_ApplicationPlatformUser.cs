namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationPlatformUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationPlatformUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Location = c.String(),
                        AvatarPicture = c.String(),
                        PredicateName = c.String(),
                        PredicateValue = c.String(),
                        PhoneNumber = c.String(),
                        EngineerName = c.String(),
                        IsHavePredicate = c.Boolean(nullable: false),
                        IsProactiveMessage = c.Boolean(nullable: false),
                        IsHaveCardCondition = c.Boolean(nullable: false),
                        CardConditionPattern = c.String(),
                        IsConditionWithAreaButton = c.Boolean(nullable: false),
                        CardConditionAreaButtonPattern = c.String(),
                        IsConditionWithInputText = c.Boolean(nullable: false),
                        CardConditionWithInputTextPattern = c.String(),
                        IsHaveSetAttributeSystem = c.Boolean(nullable: false),
                        AttributeName = c.String(),
                        CardStepPattern = c.String(),
                        StartedOn = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        TimeOut = c.DateTime(),
                        BranchOTP = c.String(),
                        TimeStamp = c.String(),
                        TypeDevice = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationPlatformUsers");
        }
    }
}

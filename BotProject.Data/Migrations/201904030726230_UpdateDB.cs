namespace BotProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MAnswers", newName: "MdAnswers");
            RenameTable(name: "dbo.MAreas", newName: "MdAreas");
            RenameTable(name: "dbo.MQuestions", newName: "MdQuestions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MdQuestions", newName: "MQuestions");
            RenameTable(name: "dbo.MdAreas", newName: "MAreas");
            RenameTable(name: "dbo.MdAnswers", newName: "MAnswers");
        }
    }
}

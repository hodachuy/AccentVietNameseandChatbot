using Microsoft.AspNet.Identity.EntityFramework;
using BotProject.Model.Models;
using System.Data.Entity;

namespace BotProject.Data
{
    public class BotDbContext : IdentityDbContext<ApplicationUser>
    {
        public BotDbContext() : base("BotDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Bot> Bots { set; get; }
        public DbSet<GroupCard> GroupCards { set; get; }
        public DbSet<FormQuestionAnswer> FormQuestionAnswers { set; get; }
        public DbSet<Answer> Answers { set; get; }
        public DbSet<Question> Questions { set; get; }
        public DbSet<QuestionGroup> QuestionGroups { set; get; }
        public DbSet<QuickReply> QuickReplys { set; get; }
        public DbSet<Image> Images { set; get; }
        public DbSet<FileCard> FileCards { set; get; }
        public DbSet<Card> Cards { set; get; }
        public DbSet<TemplateGenericItem> TemplateGenericItems { set; get; }
        public DbSet<TemplateGenericGroup> TemplateGenericGroups { set; get; }
        public DbSet<TemplateText> TemplateTexts { set; get; }
        public DbSet<ButtonLink> ButtonLinks { set; get; }
        public DbSet<ButtonPostback> ButtonPostbacks { set; get; }
        public DbSet<ButtonModule> ButtonModules { set; get; }
        public DbSet<ButtonCheck> ButtonChecks { set; get; }
        public DbSet<AIML> AIMLs { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<Setting> Settings { set; get; }
        public DbSet<Notify> Notifies { set; get; }
        public DbSet<MdArea> MdAreas { set; get; }
        public DbSet<MdQuestion> MdQuestions { set; get; }
        public DbSet <MdAnswer> MdAnswers { set; get; }
        public DbSet <ModuleCategory> ModuleCategories { set; get; }
        public DbSet<ModuleKnowledgeMedInfoPatient> ModuleKnowledgeMedInfoPatients { set; get; }
        public DbSet<Module> Modules { set; get; }
        public DbSet<MdPhone> MdPhones { set; get; }
        public DbSet<MdEmail> MdEmails { set; get; }
        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static BotDbContext Create()
        {
            return new BotDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {

            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}

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

        // Su dung migration de update ra database nen nho set set as Starup Project Web co chua chuoi connection string
        // Boi vi khi tao ra database o sql server no goi connection string o file web.config

        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<Notify> Notifies { set; get; }

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

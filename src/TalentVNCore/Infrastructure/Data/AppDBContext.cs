using TalentVN.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace TalentVN.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupAccount>().HasKey(ga => new { ga.AccountID, ga.GroupID });
            modelBuilder.Entity<NotifyGroup>().HasKey(ng => new { ng.NotifyID, ng.GroupID });
        }

        // Define DBset
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Notify> Notifys { get; set; }
        public DbSet<News> News { get; set; }

        public DbSet<GroupAccount> GroupAccounts { get; set; }
        public DbSet<NotifyGroup> NotifyGroups { get; set; }
    }
}

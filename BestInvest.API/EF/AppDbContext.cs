using BestInvest.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestInvest.API.EF
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {   }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<InvestorCategory> InvestorCategories { get; set;}
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountInfo> AccountsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().Property(p => p.MoneyCapital).HasColumnType("money");
            modelBuilder.Entity<Project>().Property(p => p.StartCapital).HasColumnType("money");
            modelBuilder.Entity<Deal>().Property(d => d.MoneyCapital).HasColumnType("money");
        }
    }
}

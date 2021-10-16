using Microsoft.EntityFrameworkCore;
using Support_Chat_App.DataAccess.Entities;

namespace Support_Chat_App.Entities
{
    public class SupportChatContext : DbContext
    {
        public SupportChatContext(DbContextOptions<SupportChatContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<AgentSeniorityType> AgentSeniorityTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<UserType>().ToTable("user_type");
            modelBuilder.Entity<AgentSeniorityType>().ToTable("agent_seniority_type");
        }
    }
}

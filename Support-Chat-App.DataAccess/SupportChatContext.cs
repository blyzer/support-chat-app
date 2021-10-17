using Microsoft.EntityFrameworkCore;
using Support_Chat_App.Data.Entities;

namespace Support_Chat_App.Data
{
    public class SupportChatContext : DbContext
    {
        public SupportChatContext(DbContextOptions<SupportChatContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<AgentSeniorityType> AgentSeniorityTypes { get; set; }
        public DbSet<TeamType> TeamTypes { get; set; }
        public DbSet<ShiftType> ShiftTypes { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<AgentChat> AgentChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<UserType>().ToTable("user_type");
            modelBuilder.Entity<AgentSeniorityType>().ToTable("agent_seniority_type");
            modelBuilder.Entity<TeamType>().ToTable("team_type");
            modelBuilder.Entity<ShiftType>().ToTable("shift_type");
            modelBuilder.Entity<Chat>().ToTable("chat");
            modelBuilder.Entity<AgentChat>().ToTable("agent_chat");
        }
    }
}

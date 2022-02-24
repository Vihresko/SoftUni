namespace FootballManager.Data
{
    using FootballManager.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-EQU1II1\SQLEXPRESS;Database=FootballManager;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPlayer>().HasKey(k => new { k.UserId, k.PlayerId });
            modelBuilder.Entity<UserPlayer>().HasOne(u => u.User).WithMany(p => p.UserPlayers);
            modelBuilder.Entity<UserPlayer>().HasOne(t => t.Player).WithMany(u => u.UserPlayers);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<UserPlayer> UserPlayers { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace RestLib.Infrastructure.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>();
            modelBuilder.Entity<Board>();
            modelBuilder.Entity<User>();
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

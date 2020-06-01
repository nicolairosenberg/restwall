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
        }

        public DbSet<Message> Messages { get; set; }
    }
}

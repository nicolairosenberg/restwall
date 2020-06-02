using Microsoft.EntityFrameworkCore;
using System;

namespace RestLib.Infrastructure.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Board>().HasData(
            //    new Board
            //    {
            //        Id = Guid.Parse("35df166a-1dca-4697-91f8-afe6499c16f4"),
            //        Title = "General Discussion",
            //        Description = "A place to be"
            //    },
            //    new Board
            //    {
            //        Id = Guid.Parse("caa7a974-3f8e-4742-8249-cd1d1e172b83"),
            //        Title = "Introduction",
            //        Description = "A place to be"
            //    },
            //    new Board
            //    {
            //        Id = Guid.Parse("c44cea98-edef-4347-84b9-731f80cb4a7b"),
            //        Title = "Reading",
            //        Description = "For every man, woman and child who loves to read"
            //    },
            //    new Board
            //    {
            //        Id = Guid.Parse("f46c14e5-422c-4d11-b73d-e233236206d8"),
            //        Title = "Writing",
            //        Description = "For the typing artists.. those who loves a good story"
            //    },
            //    new Board
            //    {
            //        Id = Guid.Parse("c3116c7a-9e42-405d-98f9-496b10e47837"),
            //        Title = "Drawing",
            //        Description = "Beauty comes in many shapes.. can you draw it?"
            //    }
            //    );

            //Guid.Parse("35df166a-1dca-4697-91f8-afe6499c16f4")


            modelBuilder.Entity<Board>();
            modelBuilder.Entity<Message>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Topic>();
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}

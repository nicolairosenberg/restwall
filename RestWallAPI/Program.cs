using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestLib.Infrastructure.Entities;

namespace RestWallAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();

                Initialize(services);
            }

            host.Run();
        }

        private static void Initialize(IServiceProvider services)
        {
            using (var context = new DataContext(
            services.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.Messages.Any())
                {
                    return;
                }

                context.Boards.Add(new Board() {
                    Id = 1,
                    Title = "General Discussion",
                    Description = "A place to be",
                });

                context.Boards.Add(new Board()
                {
                    Id = 2,
                    Title = "Introduction",
                    Description = "Another place to be",
                });

                context.Boards.Add(new Board()
                {
                    Id = 3,
                    Title = "Reading",
                    Description = "For every man, woman and child who loves to read",
                });

                context.Boards.Add(new Board()
                {
                    Id = 4,
                    Title = "Writing",
                    Description = "For the typing artists.. those who loves a good story",
                });

                context.Boards.Add(new Board()
                {
                    Id = 5,
                    Title = "Drawing",
                    Description = "Beauty comes in many shapes.. can you draw it?",
                });

                context.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

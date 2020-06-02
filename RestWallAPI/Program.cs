using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                try
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<DataContext>();

                    //context.Database.EnsureDeleted();
                    //context.Database.Migrate();

                    Initialize(services);
                }
                catch (Exception ex)
                {
                    var services = scope.ServiceProvider;
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error with Database");
                }
                
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

                context.Boards.Add(new Board()
                {
                    //Id = 1,
                    Id = Guid.Parse("35df166a-1dca-4697-91f8-afe6499c16f4"),
                    Title = "General Discussion",
                    Description = "A place to be",
                });

                context.Boards.Add(new Board()
                {
                    //Id = 2,
                    Id = Guid.Parse("caa7a974-3f8e-4742-8249-cd1d1e172b83"),
                    Title = "Introduction",
                    Description = "Another place to be",
                });

                context.Boards.Add(new Board()
                {
                    //Id = 3,
                    Id = Guid.Parse("c44cea98-edef-4347-84b9-731f80cb4a7b"),
                    Title = "Reading",
                    Description = "For every man, woman and child who loves to read",
                });

                context.Boards.Add(new Board()
                {
                    //Id = 4,
                    Id = Guid.Parse("f46c14e5-422c-4d11-b73d-e233236206d8"),
                    Title = "Writing",
                    Description = "For the typing artists.. those who loves a good story",
                });

                context.Boards.Add(new Board()
                {
                    //Id = 5,
                    Id = Guid.Parse("c3116c7a-9e42-405d-98f9-496b10e47837"),
                    Title = "Drawing",
                    Description = "Beauty comes in many shapes.. can you draw it?",
                });

                context.Users.Add(new User
                {
                    //Id = 1,
                    Id = Guid.Parse("4b7d9e48-4897-43f5-8269-58d5b2389893"),
                    Email = "nicolai.rosenberg@gmail.com",
                    Username = "Nijal",
                    Avatar = "test.png",
                    Auth0 = "saved for later",
                });

                context.Topics.Add(new Topic {
                    Id = Guid.Parse("5f7d9e48-4897-43f5-8269-58d5b2389893"),
                    BoardId = Guid.Parse("35df166a-1dca-4697-91f8-afe6499c16f4"),
                    UserId = Guid.Parse("4b7d9e48-4897-43f5-8269-58d5b2389893"),
                    Title = "A proper Greeting",
                    Text = "Please introduce yourselves."
                });

                context.Messages.Add(new Message
                {
                    Id = Guid.Parse("d977a805-f7ae-47ef-a628-f3baea486c0c"),
                    Title = "A proper greeting",
                    Text = "Hello there. Please do say hi",
                    TopicId = Guid.Parse("5f7d9e48-4897-43f5-8269-58d5b2389893"),
                    UserId = Guid.Parse("4b7d9e48-4897-43f5-8269-58d5b2389893")
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

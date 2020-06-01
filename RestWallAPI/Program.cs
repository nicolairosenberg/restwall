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

            //Continue to run the application
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

                context.Messages.AddRange(
                    new Message
                    {
                        Id = 1,
                        Title = "Candy Land"
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

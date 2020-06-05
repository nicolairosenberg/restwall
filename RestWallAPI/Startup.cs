using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {

                options.ReturnHttpNotAcceptable = true;

            }).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            .AddXmlDataContractSerializerFormatters();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "MessageBoard"));

            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<ITopicService, TopicService>();

            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected exception happened. Contact us at RestWall if this issue continues.");
                    // TODO: [NR] possible to log here
                }
                ));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

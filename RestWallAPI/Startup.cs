using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
            // NR: tried to use ETag generator to be able to improve on caching, but had to revert due to issues.. :/

            services.AddResponseCaching();

            services
            .AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.CacheProfiles.Add("360SecondsCacheProfile",
                        new CacheProfile() { Duration = 360 }
                    );
                options.CacheProfiles.Add("0SecondsCacheProfile",
                        new CacheProfile() { Duration = 0 }
                    );

            })
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(options =>
            {
                
                // NR: I want to follow standards on showing correct and detailed information of validation errors.
                // Sidenote: this approach is harder to unit test, could be easier with fluent validation, from Jeremy Skinner @ github.

                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext, context.ModelState
                        );

                    problemDetails.Detail = "See the errors field for details.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    var actionExecutingContext = context as ActionExecutingContext;

                    // NR: if modelstate have errors and arguments were correctly parsed - its validation errors:
                    if ((context.ModelState.ErrorCount > 0) && (actionExecutingContext.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://api.restwall.dk/modelvalidationproblem";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Validation errors occurred.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more errors on input occured.";

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.Configure<MvcOptions>(config => 
            {
                var newtonsoftOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if(newtonsoftOutputFormatter != null)
                {
                    newtonsoftOutputFormatter.SupportedMediaTypes.Add("application/vnd.restwall.hateoas+json");
                }
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "MessageBoard"));

            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();


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

            app.UseResponseCaching();

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

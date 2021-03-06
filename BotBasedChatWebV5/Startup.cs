﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BotBaseChatDomain.MessagesAggregate;
using BotBasedChatInfrastructure;
using BotBasedChatInfrastructure.ModelSecurity;
using BotBasedChatInfrastructure.Repositories;
using BotBasedChatWebV5.Application.Queries.Messages;
using BotBasedChatWebV5.Hubs;
using BotBasedChatWebV5.Infrastructure.AutofacModules;
using BotBasedChatWebV5.Services.Csv;
using BotBasedChatWebV5.Services.MessageBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotBasedChatWebV5
{
    public class Startup
    {
        public static IServiceProvider __serviceProvider;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>                   
                options.UseSqlServer(Configuration["ConnectionStrings:Identity"],
                sqlServerOptionsAction: sqlOptions =>
                {                    
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                }));

            services.AddIdentity<UserIdentity, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                //options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = $"/Security/Index"; //new PathString("/Security/Index");
                //options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddDbContext<AppDataContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Data"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            });

            services.AddMvc();
            services.AddSignalR();

            services.AddTransient<IMessageQueries, MessageQueries>();
            services.AddTransient<IMessageBroker, MessageBroker>();
            services.AddTransient<IMessageRepository, MessageRepository>();
             
            services.AddTransient<ICsvBot, CsvBot>();
            
            __serviceProvider = services.BuildServiceProvider();

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());            
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();


            app.UseSignalR(routes =>
            {                
                routes.MapHub<Room1Hub>("/room1Hub");
                routes.MapHub<Room2Hub>("/room2Hub");
                routes.MapHub<Room3Hub>("/room3Hub");
                routes.MapHub<Room4Hub>("/room4Hub");
            });
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help
                var userManager = (UserManager<UserIdentity>)scope.ServiceProvider.GetService(typeof(UserManager<UserIdentity>));
                // do you things here
                ApplicationDbContext.SeedAccounts(userManager, Configuration).Wait();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Biblioteczka.wwwroot.Services;
using Biblioteczka.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Biblioteczka.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Biblioteczka
{
    public class Startup
    {

        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            //ustawienie konfiguracji z pliku json dla maila
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)          //root of actual project
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();                 //pozwala na używanie innej konfiguracji ze zmiennych środowiskowych -> nadpisuje
            //W tym miejscu czyta nasza konfigurację
            _config = builder.Build();

        }
   

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //Identity
            services.AddIdentity<LibraryUser, IdentityRole>(config =>
            {
                //config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 4;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.LogoutPath = "/Auth/Logout";

                //Use Identity in the API  ?
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                   OnRedirectToLogin = async ctx =>
                   {
                       //Sprawdzenie czy zapytanie zawiera api jeżeli nieautoryzowane zapytanie zwróci kod 401 nieautoryzowane 
                       if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                       {
                           ctx.Response.StatusCode = 401;
                       }
                       else
                       {
                           ctx.Response.Redirect(ctx.RedirectUri);
                       }
                       await Task.Yield();
                   }
                };
            })
            .AddEntityFrameworkStores<LibraryContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = false;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 10;

            //});


            //implementacja IConfigurationRoot service
            services.AddSingleton(_config);


            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // Implement a real Mail Service
            }


            services.AddDbContext<LibraryContext>();

            services.AddScoped<ILibraryRepository, LibraryRepository>();

            services.AddTransient<LibraryContextSeedData>();

            services.AddLogging();

            services.AddMvc( config =>
            {
                //Jeżeli będzie w etapie produkcji a nie projektu wtedy użyć https przy logowaniu
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
                .AddJsonOptions(config =>
                {
                    // Leci lambda w celu powstanai CamelCase (system notacji ciągów tekstowych, kolejne wyrazy pisane są łącznie, każdy następy z dużej litery
                    //oprócz pierwszego. Uzycie przy API
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            LibraryContextSeedData seeder)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<BookshelveViewModel, Bookshelve>().ReverseMap();
                config.CreateMap<BookViewModel, Book>().ReverseMap();
            });


            //makes the files in web root (wwwroot by default) servable. 
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseIdentity();

            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            seeder.EnsureSeedData().Wait();

        }
    }
}

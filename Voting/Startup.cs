using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Voting.Areas.Identity.Data;
using Voting.Areas.Identity.Models;
using Voting.Areas.Identity.Models.DbContext;
using Voting.Infrastructure;
using Voting.Infrastructure.Interfaces;
using Voting.Models.DbContexts;

namespace Voting
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
            services.AddHttpContextAccessor();

            services.AddIdentity<Student, IdentityRole>().
                AddEntityFrameworkStores<StudentDbContext>().AddDefaultTokenProviders();

            services.AddDbContext<StudentDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("sqlserver"));
            });

            services.AddDbContext<ElectionDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("sqlserver"));
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/account/login";
                options.LogoutPath = "/auth/account/logout";
                options.Cookie.Name = "adfghjprince";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.Expiration = TimeSpan.FromMinutes(40);
            });

            services.AddTransient<IPictureUpload, PictureUpload>();
            services.AddSingleton<IExitingModel, ExitingModel>();
            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
  
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "identity",
                    areaName: "Identity",
                    template: "auth/{controller}/{action}"
                    );
                routes.MapRoute(
                    name: "Custom",
                    template: "admin/{controller=Categories}/{action=Details}/{catId?}"
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // Task.Run(() => SeedData.InitializeStudents(app.ApplicationServices.CreateScope().ServiceProvider));
            // Task.Run(() => SeedData.InitializeStudentRoles(app.ApplicationServices.CreateScope().ServiceProvider));
            //  Task.Run(() => SeedData.InitializeAdmins(app.ApplicationServices.CreateScope().ServiceProvider));
            //Task.Run(() => Voting.Models.Data.SeedData.InitializeVoteParams(app.ApplicationServices.CreateScope().ServiceProvider));
        }
    }
}

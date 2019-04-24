using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Voting.Areas.Identity.Data;
using Voting.Areas.Identity.Models;
using Voting.Areas.Identity.Models.DbContext;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

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
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
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
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // Task.Run(() => SeedData.InitializeStudents(app.ApplicationServices.CreateScope().ServiceProvider));
            //  Task.Run(() => SeedData.InitializeStudentRoles(app.ApplicationServices.CreateScope().ServiceProvider));
          //  Task.Run(() => SeedData.InitializeAdmins(app.ApplicationServices.CreateScope().ServiceProvider));
        }
    }
}

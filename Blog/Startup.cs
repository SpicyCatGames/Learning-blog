using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Blog.Data.FileManager;

namespace Blog
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
            services.AddControllersWithViews();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength= 6;
            })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration["DefailtConnection"]));
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileManager, FileManager>();
            // dotnet ef migrations add MigrationName
            // dotnet ef database update
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            //app.Run(async context =>
            //    await context.Response.WriteAsync("Hello World")
            //);
        }
    }
}

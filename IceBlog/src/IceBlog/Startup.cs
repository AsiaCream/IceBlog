using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IceBlog.Models;


namespace IceBlog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<BlogContext>(x => x.UseSqlServer("server=182.254.211.75;uid=sa;password=Cream2015!@#;database=iceblog"));

            services.AddEntityFrameworkSqlite()
                .AddDbContext<BlogContext>(x => x.UseSqlite("Data source=iceblog.db"));

            services.AddIdentity<User, IdentityRole<long>>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 0;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;
                x.User.AllowedUserNameCharacters = null;
            })
            .AddEntityFrameworkStores<BlogContext, long>()
            .AddDefaultTokenProviders();

            services.AddMvc();

        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Warning);
            loggerFactory.AddConsole(LogLevel.Debug);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
            
            await SampleData.InitDB(app.ApplicationServices);
        }
    }
}

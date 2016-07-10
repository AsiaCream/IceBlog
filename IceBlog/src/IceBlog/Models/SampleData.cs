using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IceBlog.Models
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var db = service.GetService<BlogContext>();

            var userManager = service.GetService<UserManager<User>>();

            var roleManager = service.GetRequiredService<RoleManager<IdentityRole<long>>>();

            if (db.Database != null && db.Database.EnsureCreated())
            {
                await roleManager.CreateAsync(new IdentityRole<long> { Name = "博主" });

                await userManager.CreateAsync(new User { UserName = "Admin" },"123456");

                db.Categorys.Add(new Category { Title = "日常", Priority = 0 });
            }
            db.SaveChanges();
        }
    }
}

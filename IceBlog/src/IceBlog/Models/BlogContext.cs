using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IceBlog.Models
{
    public class BlogContext:IdentityDbContext<User,IdentityRole<long>,long>
    {
        public BlogContext(DbContextOptions option) : 
            base(option)
        {
        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<File> Files { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<Post>(e =>
            {
                e.HasIndex(x => x.Id);
                e.HasIndex(x => x.CategoryId);
                e.HasIndex(x => x.CreateTime);
            });
            builder.Entity<File>(e =>
            {
                e.HasIndex(x => x.Id);
                e.HasIndex(x => x.CreateTime);
            });
        }

    }
}

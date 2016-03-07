using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using i_blog.Models;

namespace i_blog.DAL
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("blogdb") 
        {
            
        }

        public DbSet<Role> Roles { get; set; } 
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                        .HasMany<Role>(u => u.Roles)
                        .WithMany(r => r.Users)
                        .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("RoleRefId");
                    cs.ToTable("RoleUsers");
                });
        }

    }
}
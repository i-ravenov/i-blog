using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using i_blog.Models;

namespace i_blog.DAL
{
    public class BlogInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            var users = new List<User>
            {
                new User {Username = "username", Email = "email@user.pro", PasswordHash = "test"},
                new User {Username = "ivan", Email = "ivan@user.pro", PasswordHash = "test"}, 
                new User {Username = "dk", Email = "dk@user.pro", PasswordHash = "test"},
                new User {Username = "trans", Email = "trans@user.pro", PasswordHash = "test"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role {Name = "admin"},
                new Role {Name = "user"},
                new Role {Name = "author"}
            };

            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using i_blog.Models;

namespace i_blog.DAL
{
    public class BlogInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {

            var user1 = new User {Username = "username", Email = "email@user.pro", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test", 13) };
            var user2 = new User {Username = "ivan", Email = "ivan@user.pro", PasswordHash = "test" };
            var user3 = new User {Username = "dk", Email = "dk@user.pro", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test", 13) };
            var user4 = new User {Username = "trans", Email = "trans@user.pro", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test", 13) };

            var role1 = new Role {Name = "admin"};
            var role2 = new Role {Name = "user"};
            var role3 = new Role {Name = "author"};
            var role4 = new Role {Name =  "moderator"};

            role1.Users.Add(user1);
            role1.Users.Add(user2);
            role1.Users.Add(user3);

            user4.Roles.Add(role4);

            var users = new List<User> { user1, user2, user3, user4 };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var roles = new List<Role> { role1, role2, role3, role4 };
            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var post1 = new Post
            {
                Content = "Awesome content",
                CreatedAt = DateTime.Today,
                Slug = "slug",
                Title = "Firts Post",
                UserId = 3
            };

            var post2 = new Post
            {
                Content = "Awesome content addittionadf",
                CreatedAt = DateTime.Today,
                Slug = "slugslsllslsl",
                Title = "Second Post",
                UserId = 2
            };

            var tag1 = new Tag { Name = "programming", Slug = "programming" };
            var tag2 = new Tag { Name = "design", Slug = "design"};
            var tag3 = new Tag { Name = "thread", Slug = "thread"};

            post1.Tags.Add(tag1);
            post1.Tags.Add(tag3);

            var posts = new List<Post> { post1, post2 };
            var tags = new List<Tag> { tag1, tag2, tag3};

            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();

            tags.ForEach(t => context.Tags.Add(t));
            context.SaveChanges();
        }
    }
}
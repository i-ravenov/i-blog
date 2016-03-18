using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using i_blog.Areas.Admin.ViewModels;
using i_blog.DAL;
using i_blog.Infrastructure;
using i_blog.Models;

namespace i_blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 5;
        private BlogContext db = new BlogContext();

        public ActionResult Index(int page = 1)
        {
            var totalPostCount = db.Posts.Count();

            var currentPostPage = db.Posts
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage)
            });

        }


        public ActionResult New()
        {
            return View("form", new PostsForm
            {
                IsNew = true
            });
        }

        public ActionResult Edit(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

            return View("form", new PostsForm()
            {
                IsNew = false,
                PostId = id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title
            });
        }

        [HttpPost]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if (form.IsNew)
            {
                post = new Post()
                {
                    CreatedAt = DateTime.UtcNow,
                    UserId = 1,                     // !!! need to be redone
                };
            }
            else
            {
                post = db.Posts.Find(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            db.Posts.AddOrUpdate(post);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using i_blog.Areas.Admin.ViewModels;
using i_blog.DAL;
using i_blog.Infrastructure;
using i_blog.Infrastructure.Extensions;
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
                IsNew = true,
                Tags = db.Tags.Select(tag => new TagCheckbox
                {
                    Id = tag.TagId,
                    Name = tag.Name,
                    IsChecked = false
                }).ToList()

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
                Title = post.Title,
                Tags = db.Tags.Select(tag => new TagCheckbox
                {
                    Id = tag.TagId,
                    Name = tag.Name,
                    IsChecked = true        // must be reimplemented !!!
                }).ToList()
            });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            var selectedTags = ReconsileTags(form.Tags).ToList();

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
                foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                    post.Tags.Add(toAdd);

                foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                    post.Tags.Remove(toRemove);

            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            db.Posts.AddOrUpdate(post);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Trash(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = DateTime.UtcNow;
            db.Posts.AddOrUpdate(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = null;
            db.Posts.AddOrUpdate(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckbox> tags)
        {
            foreach (var tag in tags.Where(t => t.IsChecked))
            {
                if (tag.Id != null)
                {
                    yield return db.Tags.Find(tag.Id);
                    continue;
                }

                var existingTag = db.Tags.SingleOrDefault(t => t.Name == tag.Name);
                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Slugify()
                };

                db.Tags.AddOrUpdate(newTag);
                db.SaveChanges();
                yield return newTag;
            }
        }

    }
}
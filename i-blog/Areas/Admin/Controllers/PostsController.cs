using System.Data.Entity;
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
    }
}
using System.Linq;
using System.Web.Mvc;
using i_blog.DAL;
using i_blog.ViewModels;

namespace i_blog.Controllers
{
    public class LayoutController : Controller
    {
        private BlogContext db = new BlogContext();

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return View(new LayoutSidebar
            {
                IsLoggedIn = User.Identity.IsAuthenticated,
                UserName = User.Identity != null ? User.Identity.Name : "",
                IsAdmin = User.IsInRole("admin"),
                Tags = db.Tags.Select(tag => new
                {
                    tag.TagId,
                    tag.Name,
                    tag.Slug,
                    PostCount = tag.Posts.Count
                }).Where(t => t.PostCount > 0).OrderByDescending(p => p.PostCount).ToList()
                .Select(
                    tag => new SidebarTag(tag.TagId, tag.Name, tag.Slug, tag.PostCount))
            });
        }
    }
}
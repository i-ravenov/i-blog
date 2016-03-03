using System.Web.Mvc;

namespace it_blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return Content("ADMIN POSTS!");
        }
    }
}
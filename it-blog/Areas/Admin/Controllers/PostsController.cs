using System.Web.Mvc;

namespace it_blog.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return Content("ADMIN POSTS!");
        }
    }
}
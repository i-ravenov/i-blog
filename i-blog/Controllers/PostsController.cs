using System.Web.Mvc;

namespace i_blog.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return Content("Hello, world!");
        }
    }
}
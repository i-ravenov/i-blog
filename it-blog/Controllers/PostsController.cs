using System.Web.Mvc;

namespace it_blog.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
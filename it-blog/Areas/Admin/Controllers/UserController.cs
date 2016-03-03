using System.Web.Mvc;

namespace it_blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
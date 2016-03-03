using System.Web.Mvc;

namespace it_blog.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return Content("USERS!");
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using i_blog.DAL;

namespace i_blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index()
        {
            var model = db.Users;
            return View(model.ToList());
        }
    }
}
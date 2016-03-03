using System.Web.Mvc;
using it_blog.ViewModels;

namespace it_blog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View(new AuthLogin()
            {

            });
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            if (!ModelState.IsValid)
                return View(form);

            if (form.Username == "rainbow") return Content("The form is valid!");

            ModelState.AddModelError("Username", "Username or password isn't 20% cooler.");
            return View(form);
        }
    }
}

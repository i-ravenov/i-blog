using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using i_blog.DAL;
using i_blog.Models;
using i_blog.ViewModels;

namespace i_blog.Controllers
{
    public class AuthController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        public ActionResult Login()
        {
            return View(new AuthLogin());
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = db.Users.SingleOrDefault(u => u.Username == form.Username);

            if (user == null)
                Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is invalid"); 

            if (!ModelState.IsValid)
                return View(form);
            
            FormsAuthentication.SetAuthCookie(form.Username, true);
            
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            return RedirectToRoute("home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

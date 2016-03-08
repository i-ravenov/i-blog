using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using i_blog.Areas.Admin.ViewModels;
using i_blog.DAL;
using i_blog.Models;

namespace i_blog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }


        public ActionResult New()
        {
            return View(new NewUser
            {
            });
        }

        [HttpPost]
        public ActionResult New(NewUser form)
        {
            if (db.Users.Any(u => u.Username == form.Username) )
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User
            {
                Email = form.Email,
                Username = form.Username
            };

            user.SetPassword(form.Password);

            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(new EditUser
            {
                Username = user.Username,
                Email = user.Email
            });
        }

        [HttpPost]
        public ActionResult Edit(int id, EditUser form)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            if (db.Users.Any(u => u.Username == form.Username && u.UserID != id ) )
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.Username = form.Username;
            user.Email = form.Email;

            db.Users.AddOrUpdate(user);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            return View(new ResetPasswordUser
            {
                Username = user.Username
            });
        }

        [HttpPost]
        public ActionResult ResetPassword(int id, ResetPasswordUser form)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            form.Username = user.Username;

            if (db.Users.Any(u => u.Username == form.Username && u.UserID != id))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);
            db.Users.AddOrUpdate(user);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpPost, ActionName("delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("index");
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
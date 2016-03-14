using System.Collections.Generic;
using System.Data.Entity;
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
            return View(new IndexUser
            {
                Users = db.Users.ToList()
            });
        }


        public ActionResult New()
        {
            return View(new NewUser
            {
                Roles = db.Roles.Select(role => new RoleCheckbox
                {
                    Id = role.RoleID,
                    IsChecked = false,
                    Name = role.Name
                }).ToList()

            });
        }

        [HttpPost]
        public ActionResult New(NewUser form)
        {
            var user = new User();
            SyncRoles(form.Roles, user.Roles);

            if (db.Users.Any(u => u.Username == form.Username) )
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.Email = form.Email;
            user.Username = form.Username;

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

            var checkboxes = new List<RoleCheckbox>();

            //foreach (var role in db.Roles)
            //{
            //    checkboxes.Add(new RoleCheckbox
            //    {
            //        Id = role.RoleID,
            //        IsChecked = db.Roles.Any( r => r.RoleID == role.RoleID),
            //        Name = role.Name
            //    });
            //}
        

            EditUser edtiViewModel = new EditUser
            {
                Username = user.Username,
                Email = user.Email,
                Roles = db.Roles.Select(r => new RoleCheckbox
                {
                    Id = r.RoleID,
                    IsChecked = false,
                    Name = r.Name
                }).ToList()
            };

            return View(edtiViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditUser form)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            SyncRoles(form.Roles, user.Roles);

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


        private void SyncRoles(IList<RoleCheckbox> checkboxes, ICollection<Role> roles)
        {
            var selectedRoles = new List<Role>();

            foreach (var role in db.Roles)
            {
                var checkbox = checkboxes.Single(c => c.Id == role.RoleID);
                checkbox.Name = role.Name;

                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }

            foreach (var toadd in selectedRoles.Where(t => !roles.Contains(t)))
                roles.Add(toadd);

            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);

            db.SaveChanges();
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
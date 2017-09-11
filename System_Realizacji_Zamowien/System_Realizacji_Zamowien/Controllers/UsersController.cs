using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;
using System_Realizacji_Zamowien.ViewModel;

namespace System_Realizacji_Zamowien.Controllers
{
    public class UsersController : Controller
    {
        private Context db = new Context();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public ActionResult Create()
        {
            RegistrationViewModel model = new RegistrationViewModel();
            using (var db = new Context())
            {
                model.Accesses = new SelectList(db.Accesses.Select(x => x.Role).ToList());
                model.Firms = new SelectList(db.Firms.Select(x => x.Name).ToList());
            }
            return View(model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Context())
                {

                    db.Users.Add(new User
                    {
                        Login = model.Login,
                        Name = model.Name,
                        Surname = model.Surname,
                        Password = model.Password,
                        Phone = model.Phone,
                        Access = db.Accesses.Single(x => x.Role == model.SelectedAccess),
                        Employer = db.Firms.Single(x => x.Name == model.SelectedFirm)
                    });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            else
            {
                using (var db = new Context())
                {

                    model = new RegistrationViewModel
                    {
                        Accesses = new SelectList(db.Accesses.Select(x => x.Role).ToList()),
                        Firms = new SelectList(db.Firms.Select(x => x.Name).ToList())
                    };
                }
                return View(model);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
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
            RegistrationViewModel model = new RegistrationViewModel
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Surname = user.Surname,
                Phone = user.Phone,
                SelectedAccess = user.Access.Role,
                SelectedFirm = user.Employer.Name,
                Accesses = new SelectList(db.Accesses.Select(x => x.Role).ToList()),
                Firms = new SelectList(db.Firms.Select(x => x.Name).ToList())
            };

            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistrationViewModel model)
        {
            User user = new Models.User
            {
                Id = model.Id,
                Login = model.Login,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password,
                Phone = model.Phone,
                Access = db.Accesses.Single(x => x.Role == model.SelectedAccess),
                Employer = db.Firms.Single(x => x.Name == model.SelectedFirm)
            };

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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

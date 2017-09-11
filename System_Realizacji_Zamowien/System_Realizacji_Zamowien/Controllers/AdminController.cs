using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;
using System_Realizacji_Zamowien.ViewModel;

namespace System_Realizacji_Zamowien.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Dealer()
        {
            Firm corp;
            using (var db = new Context())
            {
                corp = db.Users.First(x => x.Access.Role == "Admin").Employer;
                if (corp != null)
                {
                    db.Firms.Add(new Firm());
                    db.SaveChanges();
                }
            }
            return View(corp);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Dealer(Firm model)
        {
            using (var db = new Context())
            {
                db.Entry<Firm>(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            using (var db = new Context())
            {
                db.Users.Remove(db.Users.Single(x => x.Id == id));
                db.SaveChanges();
            }
            return RedirectToAction("AdminManage", "Account");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.ViewModel;
using System_Realizacji_Zamowien.Models;
using System.Web.Security;

namespace System_Realizacji_Zamowien.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel Model)
        {

            if (ModelState.IsValid)
            {
                using (var db = new Context())
                {
                    if (db.Users.Any(x => x.Login == Model.Login && x.Password == Model.Password))
                    {
                        var user = db.Users.Single(x => x.Login == Model.Login);
                        FormsAuthentication.SetAuthCookie(Model.Login, true);
                        SetRoleCookie(user);
                        string name = User.Identity.Name;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Info"] = "Nie znaleziono użytkownika o podanym loginie, lub hasło się nie zgadza";
                        return RedirectToAction("Login", "Account");

                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void SetRoleCookie(User user)
        {
            var cookie = this.ControllerContext.HttpContext.Response.Cookies["Role"];
            if (cookie != null)
            {
                cookie.Value = user.Access.Role;
            }
            else
            {
                cookie = new HttpCookie("Role");
                cookie.Value = user.Access.Role;
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }
       
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Registration()
        {
            User user = null;
            using (var db = new Context())
            {
                user = db.Users.Single(x => x.Login == User.Identity.Name);
                if (user.Access.Role == "Admin")
                {

                    RegistrationViewModel model;
                    model = new RegistrationViewModel
                    {
                        Accesses = new SelectList(db.Accesses.Select(x => x.Role).ToList()),
                        Firms = new SelectList(db.Firms.Select(x => x.Name).ToList())
                    };
                    return View(model);

                }
                else
                {
                    TempData["Info"] = "Nie masz dostępu do tworzenia Kont";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
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
                return RedirectToAction("Index", "Home");
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
    }
}
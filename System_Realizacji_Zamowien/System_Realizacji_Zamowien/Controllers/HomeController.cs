using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;
using System_Realizacji_Zamowien.ViewModel;

namespace System_Realizacji_Zamowien.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["count"] = 10;
            var role = Request.Cookies["role"];
            if (User.Identity.IsAuthenticated && role != null)
            {
                switch (role.Value)
                {
                    case "User":
                        return RedirectToAction("Products", "Products");
                        break;
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                        break;
                    case "Operator":
                        return RedirectToAction("Index", "Admin");
                        break;
                    case "Dealer":
                        return RedirectToAction("Orders", "Order");
                        break;
                    default:
                        return RedirectToAction("Login", "Account");
                        break;
                }
                
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Messages()
        {
            IEnumerable<MessageViewModel> model;
            using (var db = new Context())
            {
                model = db.Users.Include("Messages").Single(x=> x.Login == User.Identity.Name).Messages != null ? db.Users.Include("Messages").Single(x=> x.Login == User.Identity.Name).Messages.Select(x => new MessageViewModel
                {
                    Content = x.Content,
                    Date = x.Date,
                    Id = x.Id,
                    Url = x.Url
                }).ToList() : new List<MessageViewModel>();
            }
            return PartialView("_MessagesPartialView", model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
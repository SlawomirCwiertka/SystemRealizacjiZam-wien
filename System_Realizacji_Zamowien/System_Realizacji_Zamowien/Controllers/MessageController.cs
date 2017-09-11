using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;
using System_Realizacji_Zamowien.ViewModel;

namespace System_Realizacji_Zamowien.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Messages()
        {
            IEnumerable<MessageViewModel> model;
            using (var db = new Context())
            {
                var user = db.Users.Single(x => x.Login == User.Identity.Name);
                model = db.Users.Include("Messages").Single(x => x.Login == User.Identity.Name).Messages != null ? db.Users.Include("Messages").Single(x => x.Login == User.Identity.Name).Messages.Select(x => new MessageViewModel
                {
                    Content = x.Content,
                    Date = x.Date,
                    Id = x.Id,
                    Url = x.Url,
                    IsRead = db.States.SingleOrDefault(z => z.Message.Id == x.Id && z.User.Id == user.Id ) != null && db.States.Single(z => z.Message.Id == x.Id && z.User.Id == user.Id ).Flag == Flag.IsRead ? true: false
                }).OrderByDescending(x => x.Date).ToList() : new List<MessageViewModel>();
                
            }
            return PartialView("_MessagesPartialView", model);
        }
        public ActionResult Remove(int id)
        {
            using(var db = new Context())
            {
                var user = db.Users.Include("Messages").Include("States").Include("States.Message").Single(x => x.Login == User.Identity.Name);
                var states = user.States.Where(x => x.Message!= null && x.Message.Id == id).ToList();
                db.States.RemoveRange(states);
                user.Messages.Remove(db.Messages.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
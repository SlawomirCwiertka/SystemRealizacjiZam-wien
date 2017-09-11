using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Helpers;
using System_Realizacji_Zamowien.Models;
using System_Realizacji_Zamowien.ViewModel;

namespace System_Realizacji_Zamowien.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Orders(int id = 1)
        {
            OrdersViewModel model = new OrdersViewModel();
            using (var db = new Context())
            {
                var user = db.Users.Single(x => x.Login == User.Identity.Name);
                var orders = db.Orders.Include("User").Include("Invoices").Select(x => new OrderViewModel()
                {
                    Id = x.Id,
                    DataOrder = x.DataOrder,
                    Success = x.Success,
                    User = x.User,
                    Invoices = x.Invoices.ToList(),
                    IsRead = db.States.FirstOrDefault(z => z.Order.Id == x.Id && z.User.Id == user.Id) != null && db.States.FirstOrDefault(z => z.Order.Id == x.Id && z.User.Id == user.Id).Flag == Flag.IsRead ? true : false
                }).OrderByDescending(x => x.DataOrder).ToList();
                model.Orders = orders.Skip((id - 1) * 10).Take((int)Session["count"]).ToList();
                model.Count = orders.Count;
            }

            return View(model);
        }
        //
        // GET: /Order/
        public ActionResult AddOrder()
        {
            OrdersViewModel model = new OrdersViewModel();
            using (var db = new Context())
            {
                var user = db.Users.Include("ShoppingCart").Include("Orders").Include("Orders.Items").Include("Orders.User").Include("Orders.Invoices").Single(x => x.Login == User.Identity.Name);
                var order = db.Orders.Add(new Order { User = user, DataOrder = DateTime.Now, Items = user.ShoppingCart.Where(x => !x.InProgress).ToList() });
                user.ShoppingCart.ToList().ForEach(x => x.InProgress = true);
                db.States.Add(new State { Order = order, User = user, Flag = Flag.IsVisible });
                db.SaveChanges();
                var message = db.Messages.Add(new Message
                {
                    Content = String.Format("Użytkownik {0} dodał zamówienie", user.Login),
                    Date = DateTime.Now,
                    Url = String.Format("Order/Order/{0}", order.Id)
                });
                db.States.Add(new State { User = user, Message = message, Flag = Flag.IsVisible });
                var users = db.Accesses.Include("Users").Include("Users.Messages").Single(x => x.Role == "Dealer").Users;
                users.Where(x => x.Messages == null).ToList().ForEach(x => x.Messages = new List<Message>());
                users.ToList().ForEach(x => x.Messages.Add(message));
                users.ToList().ForEach(x => db.States.Add(new State{ Order = order, User = x, Flag = Flag.IsVisible }));
                users.ToList().ForEach(x => db.States.Add(new State { Message = message, User = x, Flag = Flag.IsVisible }));
                db.SaveChanges();
            }
            return RedirectToAction("MyOrders");
        }
        public ActionResult MyOrders(int id = 1)
        {
            OrdersViewModel model = new OrdersViewModel();
            using (var db = new Context())
            {
                var user = db.Users.Include("ShoppingCart").Include("Orders").Include("Orders.Items").Include("Orders.User").Include("Orders.Invoices").Single(x => x.Login == User.Identity.Name);
                var orders = user.Orders.Where(x => !x.Success).Select(x => new OrderViewModel()
                {
                    Id = x.Id,
                    DataOrder = x.DataOrder,
                    Success = x.Success,
                    User = x.User,
                    Invoices = x.Invoices.ToList(),
                    IsRead = db.States.FirstOrDefault(z => z.Order.Id == x.Id && z.User.Id == user.Id) != null && db.States.Single(z => z.Order.Id == x.Id && z.User.Id == user.Id).Flag == Flag.IsRead ? true : false
                }).OrderByDescending(x => x.DataOrder).ToList();
                model.Orders = orders.Skip((id - 1) * 10).Take((int)Session["count"]).ToList();
                model.Count = orders.Count;
            }
            return View("Orders", model);
        }

        public ActionResult Order(int id, int? messageId)
        {
            OrderViewModel model = new OrderViewModel();
            using (var db = new Context())
            {
                var user = db.Users.Include("Messages").Single(x => x.Login == User.Identity.Name);
                if (messageId!= null)
                {
                    db.States.Single(z => z.Message.Id == messageId && z.User.Id == user.Id).Flag = Flag.IsRead;
                }
                var order = db.Orders.Include("Items").Include("Items.Product").Include("Items.User").Include("Invoices").Include("Invoices.Positions").Include("Invoices.Positions.Product").Single(x => x.Id == id);
                db.States.Single(z => z.Order.Id == order.Id && z.User.Id == user.Id).Flag = Flag.IsRead;
                model.DataOrder = order.DataOrder;
                model.Id = order.Id;
                model.Invoices = order.Invoices.ToList();
                model.Items = order.Items.Select(x => new ItemCardViewModel
                {
                    Count = x.Count,
                    Id = x.Id,
                    Product = x.Product,
                    ToInvoice = x.Count - model.Invoices.Where(y => y.Positions.Count > 0 && y.Positions.Any(z => z.Product.Id == x.Product.Id)).Sum(y => y.Positions.Single(z => z.Product.Id == x.Product.Id).Count),
                    User = x.User
                }).ToList();
                model.Success = order.Success;
                db.SaveChanges();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddInvoice(OrderViewModel model)
        {
            using (var db = new Context())
            {
                var invoice = db.Invoices.Add(new Invoice
                {
                    Date = DateTime.Now,
                    Term = DateTime.Now.AddDays(model.TimeToPay),
                    Order = db.Orders.Include("User").Single(x => x.Id == model.Id),
                    Positions = model.Items.Where(x => x.ToInvoice > 0).Select(x => new ItemCard
                    {
                        Count = x.ToInvoice,
                        InProgress = true,
                        Product = db.Products.Find(x.Product.Id),
                        User = db.Users.Find(x.User.Id)
                    }).ToList()
                });
                db.SaveChanges();
                var user = db.Users.Include("Messages").Include("Orders").Include("Orders.Invoices").Include("Orders.Invoices.Positions").Include("Orders.Items").Single(x => x.Id == invoice.Order.User.Id);
                if (user.Messages == null)
                {
                    user.Messages = new List<Message>();
                }
                var message = db.Messages.Add(new Message
                {
                    Content = "Została wystawiona faktura",
                    Date = DateTime.Now,
                    Url = String.Format("Order/Order/{0}", model.Id)
                });
                user.Messages.Add(message);
                db.States.Add(new State { User = user, Message = message, Flag = Flag.IsVisible });
                int sum = 0;
                user.Orders.Single(x => x.Id == model.Id).Invoices.ToList().ForEach(x => sum += x.Positions.Sum(z => z.Count));
                if (user.Orders.Single(x => x.Id == model.Id).Items.Sum(x => x.Count) == sum)
                {
                    var message2 = db.Messages.Add(new Message
                    {
                        Content = "Zamówienie zostało zrealizowane",
                        Date = DateTime.Now,
                        Url = String.Format("Order/Order/{0}", model.Id)
                    });
                    user.Messages.Add(message2);
                    db.States.Add(new State { User = user, Message = message2, Flag = Flag.IsVisible });
                }
                db.SaveChanges();
            }
            return RedirectToAction("Orders");
        }
        public ActionResult Invoice(int id)
        {

            return View(GetInvoiceViewModel(id));
        }


        public ActionResult DownloadInvoice(int id)
        {
            var model = GetInvoiceViewModel(id);
            return File(HtmlToPdfConverter.DownloadPdf(this, "DownloadInvoice", model), ".pdf", String.Format("Faktura{0}.pdf", model.Date.ToShortDateString()));
        }

        private InvoiceViewModel GetInvoiceViewModel(int id)
        {
            InvoiceViewModel model = new InvoiceViewModel();
            using (var db = new Context())
            {
                var invoice = db.Invoices.Include("Order").Include("Order.User").Include("Order.User.Employer").Include("Positions").Include("Positions.Product").Include("Positions.Product.UnitOfMeasure").Single(x => x.Id == id);
                model.Id = invoice.Id;
                model.Date = invoice.Date;
                model.OrderId = invoice.Order.Id;
                model.Positions = invoice.Positions.Select(x => new ItemCardViewModel
                {
                    Count = x.Count,
                    Id = x.Id,
                    Product = x.Product,
                    UnitOfMeasure = x.Product.UnitOfMeasure.Name,
                }).ToList();
                model.Term = invoice.Term;
                model.User = new SmallUserViewModel
                {
                    Id = invoice.Order.User.Id,
                    Surname = invoice.Order.User.Surname,
                    Login = invoice.Order.User.Login,
                    Name = invoice.Order.User.Name,
                    Phone = invoice.Order.User.Phone,
                    FirmName = invoice.Order.User.Employer.Name,
                    FirmNip = invoice.Order.User.Employer.Nip,
                    FirmAdress = invoice.Order.User.Employer.City + invoice.Order.User.Employer.Adress,
                    FirmRegon = invoice.Order.User.Employer.Regon,
                    FirmPostCode = invoice.Order.User.Employer.PostCode
                };
                model.Dealer = db.Users.First(x => x.Access.Role == "Admin").Employer;
            }
            return model;
        }

        public ActionResult Remove(int id)
        {
            using (var db = new Context())
            {
                var order = db.Orders.Find(id);
                order.Items.ToList().ForEach(x => db.Products.Find(x.Product.Id).Availability += x.Count);
                var states = db.States.Where(x => x.Order.Id == id || x.Message.Url == "Order/Order/" + id).ToList();
                db.States.RemoveRange(states);
                var messages = db.Messages.Where(x => x.Url == "Order/Order/" + id).ToList();
                db.Messages.RemoveRange(messages);
                db.Orders.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("Orders");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.ViewModel;
using System_Realizacji_Zamowien.Models;
namespace System_Realizacji_Zamowien.Controllers
{
    public class ItemCardController : Controller
    {
        //
        // GET: /ItemCard/

        public ActionResult AddItemCard()
        {
            return View();
        }
        [HttpPost]
        public void AddItemCard(int id, int count)
        {
            using (var db = new Context())
            {
                var product = db.Products.Single(x => x.Id == id);
                var user = db.Users.Include("ShoppingCart").Single(x => x.Login == User.Identity.Name);
                    product.Availability -= count;
                    if (user.ShoppingCart.Any(x => !x.InProgress && x.Product.Id == id))
                {
                    user.ShoppingCart.Single(x => !x.InProgress && x.Product.Id == id ).Count += count;
                }
                else
                {
                    user.ShoppingCart.Add(new ItemCard {
                    Product = product,
                    User = user,
                    Count = count
                });
                }
                db.SaveChanges();
            }
        }

        public int GetItemCardCount()
        {
            int result;
            using (var db = new Context())
            {
                if ( !String.IsNullOrEmpty(User.Identity.Name))
                {
                    result = db.Users.Include("ShoppingCart").Single(x => x.Login == User.Identity.Name).ShoppingCart.Where(x => !x.InProgress).ToList().Count;

                }
                else
                    result = 0;
            }
            return result;
        }
        

        [Authorize]
        public ActionResult ShoppingCard(){

            IEnumerable<ItemCardViewModel> model;
            User user;
            using (var db = new Context())
            {
               user  = db.Users.Include("ShoppingCart").Include("ShoppingCart.Product").Single(x => x.Login == User.Identity.Name);
            }
            if (user.ShoppingCart == null)
            {
                user.ShoppingCart = new List<ItemCard>();
            }
            model = user.ShoppingCart.Where(x => !x.InProgress).Select(x => new ItemCardViewModel
            {
                Count = x.Count,
                Id = x.Id,
                Product = x.Product,
                User = x.User
            });
            return View(model.ToList());
        }
        public ActionResult Remove(int id)
        {
            using (var db = new Context())
            {

                var user = db.Users.Include("ShoppingCart").Include("ShoppingCart.Product").Single(x => x.Login == User.Identity.Name);
                var itemcard = user.ShoppingCart.Single(x => x.Id == id);

                var product = db.Products.Single(x => x.Id == itemcard.Product.Id);
                product.Availability += itemcard.Count;
                db.ItemCards.Remove(db.ItemCards.Single(x=>x.Id == id));
                db.SaveChanges();
            }

            return RedirectToAction("ShoppingCard", "ItemCard", null);

        }
	}
}
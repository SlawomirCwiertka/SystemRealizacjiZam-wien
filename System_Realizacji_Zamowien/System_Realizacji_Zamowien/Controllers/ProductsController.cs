using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.ViewModel;
using System_Realizacji_Zamowien.Models;
using System.Net;
using System.Data.Entity;
using System.Web.Helpers;
using System.IO;

namespace System_Realizacji_Zamowien.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Products(int? page, int? category, int? count, string searchText)
        {
            int countItem = count.HasValue ? count.Value : 10;
            Session["count"] = countItem;
            Session["text"] = searchText;
            Session["category"] = category;
            int pageIndex = page != null ? page.GetValueOrDefault() : 1;
            ProductsViewModel model = new ProductsViewModel();
            using (var db = new Context())
            {
                var subCategories = category != null ? GetAllDescendants(db.Categories.Single(x => x.Id == category)).ToList() : new List<int>();
                model.CountAll = db.Products.Where(x => (category != null ? x.Category.Id == category || subCategories.Any(z => x.Category.Id == z) : true))
                 .OrderBy(x => x.Id).Count();
                model.SetCount = countItem != 0 ? countItem : model.CountAll;
                model.Products = db.Products.Include("Category").Where(x => (category != null ? x.Category.Id == category || subCategories.Any(z => x.Category.Id == z) : true))
                    .OrderBy(x => x.Id).Skip(countItem * (pageIndex - 1)).Take(model.SetCount).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchText) || !string.IsNullOrEmpty(searchText))
            {
                model.Products = model.Products.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper())).ToList();
            }
            return View(model);

        }

        public ActionResult Product(int id)
        {
            ProductViewModel model;
            using (var db = new Context())
            {
                var product = db.Products.Single(x => x.Id == id);


                model = new ProductViewModel()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Photo = product.LinkPhoto,
                    Availability = product.Availability,
                    SelectPrimaryCategory = product.Category.Name,
                    Id = product.Id

                };
            }
            return View(model);
        }
        public ActionResult Image(int id)
        { 
            ProductViewModel model;
            using (var db = new Context())
            {
                var product = db.Products.Single(x => x.Id == id);
                model = new ProductViewModel
                {
                    Id = product.Id,
                    Photo = product.LinkPhoto,
                    Name = product.Name
                };
            }

            return View(model);
        }
        private IEnumerable<int> GetAllDescendants(Category parent)
        {
            List<int> result = new List<int>();

            if (parent.SubCategories != null && parent.SubCategories.Any())
            {
                result.AddRange(parent.SubCategories.Select(x => x.Id));
                parent.SubCategories.ToList().ForEach(x => result.AddRange(GetAllDescendants(x)));
            }

            return result;
        }
        private Context db = new Context();

        // GET: Products1
        public ActionResult Index()
        {
            return View(db.Products.ToList().Select(x => new ProductViewModel
            {
                Availability = x.Availability,
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Photo = x.LinkPhoto,
                Price = x.Price,
                Vat = x.Vat
            }));
        }

        // GET: Products1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products1/Create
        public ActionResult Create()
        {
            ProductViewModel model;
            using (var db = new Context())
            {
                var category = db.Categories.Where(x => x.Name != "Root").Select(x => x.Name).ToList();
                var units = db.Units.Select(x => x.Name).ToList();
                model = new ProductViewModel
                {
                    PrimaryCategories = new SelectList(category),
                    UnitsOfMeasure = new SelectList(units)
                };

            }
            return View(model);
        }

        // POST: Products1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            using (var db = new Context())
            {
                var category = db.Categories.Select(x => x.Name).ToList();
                var units = db.Units.Select(x => x.Name).ToList();
                model.PrimaryCategories = new SelectList(category);
                model.UnitsOfMeasure = new SelectList(units);
            }
            if (ModelState.IsValid)
            {
                using (var db = new Context())
                {
                    WebImage image = WebImage.GetImageFromRequest();
                    if (image == null)
                    {
                        TempData["Info"] = "Dodaj Zdjęcie";
                        return View(model);
                    }
                    else
                    {
                        image.Resize(400, 400);
                        var fileName = Path.GetFileName(image.FileName);
                        var linktobase = string.Format("/Image/{0}{1}", User.Identity.Name, fileName);
                        String path = Server.MapPath(linktobase);
                        image.Save(path);
                        db.Products.Add(new Product
                          {

                              Name = model.Name,
                              Description = model.Description,
                              Price = model.Price,
                              LinkPhoto = linktobase,
                              Availability = model.Availability,
                              Category = db.Categories.Single(x => x.Name == model.SelectPrimaryCategory),
                              UnitOfMeasure = db.Units.Single(x => x.Name == model.SelectUnit),
                              Vat = model.Vat / 100
                          });
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        // GET: Products1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            product.Vat *= 100;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Context())
                {
                    WebImage image = WebImage.GetImageFromRequest();
                    if (image != null)
                    {
                        image.Resize(400, 400);
                        var fileName = Path.GetFileName(image.FileName);
                        var linktobase = string.Format("/Image/{0}{1}", User.Identity.Name, fileName);
                        String path = Server.MapPath(linktobase);
                        image.Save(path);
                        product.LinkPhoto = linktobase;
                    }
                    product.Vat = product.Vat / 100;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: Products1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
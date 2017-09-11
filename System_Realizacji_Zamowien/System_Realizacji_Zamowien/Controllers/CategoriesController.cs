using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;
using System_Realizacji_Zamowien.ViewModel;

namespace System_Realizacji_Zamowien.Controllers
{
    public class CategoriesController : Controller
    {

        public ActionResult ShowCategories()
        {
            var model = new SideBarMenuViewModel();

            using (var db = new Context())
            {
                model.MenuName = "Fajne Menu";

                var categorylist = db.Categories.Single(x => x.Id == 4).SubCategories.Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                model.SideBarMenu = categorylist;
            }

            return PartialView(model);
        }

        private CategoryViewModel GetCategoryViewModel(Category category)
        {
            List<CategoryViewModel> sub = null;

            if (category.SubCategories != null && category.SubCategories.Any())
            {
                sub = category.SubCategories.Select(m => GetCategoryViewModel(m)).ToList();
            }

            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                SubCategories = sub,
                Photo = category.LinkPhoto
            };
        }
        public ActionResult SubCategories(int Id)
        {
            var model = new CategoryViewModel();
            using (var db = new Context())
            {
                var category = db.Categories.SingleOrDefault(x => x.Id == Id);
                model.Id = category.Id;
                model.Name = category.Name;
                model.Photo = category.LinkPhoto;
                model.SubCategories = category.SubCategories.Select(x => GetCategoryViewModel(x)).ToList();
            }
            return View(model);
        }
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }


            return View(category);
        }

        public ActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel
                {
                    Categories = new SelectList(db.Categories.Select(x => x.Name).ToList())
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                WebImage image = WebImage.GetImageFromRequest();
                if (image == null)
                {
                    TempData["Info"] = "Dodaj Zdjęcie";
                    return RedirectToAction("Create", category);
                }
                else
                {
                    image.Resize(400, 400);
                    var fileName = Path.GetFileName(image.FileName);
                    var linktobase = string.Format("/Image/Categories/{0}", fileName);
                    String path = Server.MapPath(linktobase);
                    image.Save(path);
                    category.Photo = linktobase;
                    db.Categories.Add(new Category
                    {
                        LinkPhoto = category.Photo,
                        Name = category.Name,
                        Parent = db.Categories.Single(x => x.Name == category.ParentCategory)
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            CategoryViewModel model = new CategoryViewModel
            {
                Categories = new SelectList(db.Categories.Where(x => x.Id != category.Id).Select(x => x.Name).ToList()),
                Id = category.Id,
                Name = category.Name,
                Photo = category.LinkPhoto,
                ParentCategory = category.Parent.Name
            };
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                WebImage image = WebImage.GetImageFromRequest();
                if (image != null)
                {
                    image.Resize(400, 400);
                    var fileName = Path.GetFileName(image.FileName);
                    var linktobase = string.Format("/Image/Categories/{0}", fileName);
                    String path = Server.MapPath(linktobase);
                    image.Save(path);
                    category.Photo = linktobase;
                }

                Category model = db.Categories.Find(category.Id);
                    model.LinkPhoto = category.Photo;
                    model.Name = category.Name;
                    model.Parent = db.Categories.Single(x => x.Name == category.ParentCategory);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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

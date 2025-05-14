using FourthPracticeNim.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourthPracticeNim.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();
       
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var data = db.Products.Include(p => p.Category)
                                  .OrderBy(p => p.ProductId)
                                  .ToPagedList(page, pageSize);

            ViewBag.CurrentPageSize = pageSize;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product pp)
        {
            if (ModelState.IsValid == true)
            {
                db.Products.Add(pp);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.insert = "<script>alert('data inserted')</script>";
                    ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.insert = "<script>alert('data not inserted')</script>";
                }


            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            var data = db.Products.Where(model => model.CategoryId == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Product pp)
        {
            db.Entry(pp).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
                ViewBag.update = "<script>alert('data update')</script>";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.update = "<script>alert('data not updated')</script>";
            }

            return View();
        }
        public ActionResult Delete(int id)
        {
            var data = db.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product pp)
        {
            var product = db.Products.Find(pp.ProductId);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    
}
}
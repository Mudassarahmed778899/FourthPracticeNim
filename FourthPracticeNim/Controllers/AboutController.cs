using FourthPracticeNim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourthPracticeNim.Controllers
{
    public class AboutController : Controller
    {
        AppDbContext db = new AppDbContext();
        // GET: About
        public ActionResult Index()
        {
            var data = db.Categories.ToList();
            return View(data);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Category pp)
        {
            db.Categories.Add(pp);
            int a = db.SaveChanges();
            if (a > 0)
            {
                ViewBag.insert = "<script>alert('data inserted')</script>";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.insert = "<script>alert('data not inserted')</script>";
            }

            return View();
        }
        public ActionResult Edit(int id)
        {
            var data = db.Categories.Where(model => model.CategoryId == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Category pp)
        {
            db.Entry(pp).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
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
            var data = db.Categories.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category pp)
        {
            var category = db.Categories.Find(pp.CategoryId);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

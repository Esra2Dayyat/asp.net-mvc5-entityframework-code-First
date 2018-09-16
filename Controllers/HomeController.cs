using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using amazon.Models;

namespace amazon.Controllers
{
    public class HomeController : Controller
    {
        private CustomerContext db = new CustomerContext();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Imgeuploads.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imgeupload imgeupload = db.Imgeuploads.Find(id);
            if (imgeupload == null)
            {
                return HttpNotFound();
            }
            return View(imgeupload);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImgId,ImgPath")] Imgeupload imgeupload)
        {
            if (ModelState.IsValid)
            {
                db.Imgeuploads.Add(imgeupload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imgeupload);
        }

        // GET: Home/Edit/5
        public ActionResult Order(int id , string imgpath)
        {
        
            return View();
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImgId,ImgPath")] Imgeupload imgeupload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imgeupload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imgeupload);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imgeupload imgeupload = db.Imgeuploads.Find(id);
            if (imgeupload == null)
            {
                return HttpNotFound();
            }
            return View(imgeupload);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imgeupload imgeupload = db.Imgeuploads.Find(id);
            db.Imgeuploads.Remove(imgeupload);
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

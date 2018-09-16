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
    public class OrdersController : Controller
    {
        private CustomerContext db = new CustomerContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customeres).Include(o => o.GetOrder).Include(o => o.shipToAdress);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {

            List<Order> OrderCustomer = db.Orders.Where(emp => emp.Customerid == id).ToList();
            if (OrderCustomer == null)
            {
                return HttpNotFound();
            }


            var itemsInCart = from o in db.Orders
                              where o.Customerid == id
                              select new { o.GetOrder.Price };
            var sum = itemsInCart.ToList().Select(c => c.Price).Sum();

            ViewData["totalsum"] = sum;



            return View(OrderCustomer);
        }

        // GET: Orders/Create 
        public ActionResult Create(int id)
        {

            var model = db.Imgeuploads.Single(emp => emp.ImgId == id);
            ViewBag.Adress= new SelectList(db.Adresses, "AdressId", "City" );

            ViewData["imgprice"] = model.Price;
            TempData["selectorder"] = model.Select_Order;
            ViewBag.imgpath = model.ImgPath;
            ViewBag.ProductID = model.ImgId;

            var model2 = new Order();
            return View(model2);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(int id, Order order)
        {
            int iduser = Convert.ToInt32(Session["iduser"]);
            order.Customerid = iduser;
            ViewBag.Adress = new SelectList(db.Adresses, "AdressId", "City");

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customerid = new SelectList(db.Customers, "Customerid", "Namee", order.Customerid);
            ViewBag.ImgId = new SelectList(db.Imgeuploads, "ImgId", "ImgPath", order.ImgId);
            ViewBag.Adress = new SelectList(db.Adresses, "AdressId", "City");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Customerid,AdressId,OrderDate,ImgId,Totalorder")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customerid = new SelectList(db.Customers, "Customerid", "Namee", order.Customerid);
            ViewBag.ImgId = new SelectList(db.Imgeuploads, "ImgId", "ImgPath", order.ImgId);
            ViewBag.AdressId = new SelectList(db.Adresses, "AdressId", "City", order.AdressId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

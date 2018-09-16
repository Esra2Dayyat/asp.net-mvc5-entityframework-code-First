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
    public class CustomersController : Controller
    {
        CustomerContext db = new CustomerContext();

        [HttpGet]

        [ActionName("SignIn")]
        public ActionResult SignIn()
        {


            return View();

        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult SignIn([Bind(Include = "id,Namee,Email,Password,ConfirmPassword")]Customer obj)
        {
            Customer validate = (from user in db.Customers
                                 where user.Email.Equals(obj.Email)
                                 && user.Password.Equals(obj.Password)
                                 select user).FirstOrDefault();


            if (validate != null)
            {
                Customer obj1 = db.Customers.Single(emp => emp.Email == obj.Email);
                Session["nameuser"] = obj1.Namee;
                Session["iduser"] = obj1.Customerid;
                return RedirectToAction("Index", "Home");

            }


            ViewBag.validatemassage = "UserName OR password is invaid";
            return View();


        }

        [HttpGet]
        public ActionResult Signup()
        {

            Session["nameuser"] = null;
            Session["iduser"] = null;
            return RedirectToAction("Index", "Home");

        }



        // GET: costumers/Create
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,Namee,Email,Password,ConfirmPassword")]Customer Customer)
        {
            if (db.Customers.Any(emp => emp.Email == Customer.Email))
            {
                ViewBag.DuplicateMassage = "this Email  Already exit";
                return View("create", Customer);

            }
            db.Customers.Add(Customer);
            db.SaveChanges();

            return RedirectToAction("SignIn");

        }


        [HttpGet]
      
        public ActionResult Update_account(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            ViewBag.Adress = new SelectList(db.Adresses, "AdressId", "City" );


            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
       
        public ActionResult Update_account([Bind(Include = "Customerid,Namee,Email,Password,ConfirmPassword,Phone,AdressId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
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

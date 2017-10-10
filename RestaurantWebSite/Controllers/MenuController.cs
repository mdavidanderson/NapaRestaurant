using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWebSite.Models;

namespace RestaurantWebSite.Controllers
{
    public class MenuController : Controller
    {
        private RestaurantDB db = new RestaurantDB();

        // GET: Breakfast Menu
        public ActionResult Breakfast()
        {
            var breakfasts = from RestaurantDB in db.Items
                             where RestaurantDB.ItemCategory == "Breakfast"
                             select RestaurantDB;

 
            return View(breakfasts.ToList());
        }

        // GET: Breakfast Menu/Details/
        public ActionResult BreakfastDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Lunch Menu
        public ActionResult Lunch()
        {
            var lunches = from RestaurantDB in db.Items
                             where RestaurantDB.ItemCategory == "Lunch"
                             select RestaurantDB;


            return View(lunches.ToList());
        }

        // GET: Lunch Menu/Details/
        public ActionResult LunchDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }


        // GET: Dinner Menu
        public ActionResult Dinner()
        {
            var dinners = from RestaurantDB in db.Items
                          where RestaurantDB.ItemCategory == "Dinner"
                          select RestaurantDB;


            return View(dinners.ToList());
        }

        // GET: Dinner Menu/Details/
        public ActionResult DinnerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
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

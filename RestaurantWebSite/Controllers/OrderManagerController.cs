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
    //[Authorize(Roles ="Manager")]
    [Authorize]
    public class OrderManagerController : Controller
    {
        private RestaurantDB db = new RestaurantDB();

        // GET: OrderManager
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                var orders = db.Orders.Include(o => o.OrderDetails);
                return View(orders);
            }
            else
            {
                var orders = db.Orders.Where(o => o.Username == User.Identity.Name);
                return View(orders);
            }

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetails = db.OrderDetails.Where(o => o.OrderID == id).Include(o => o.Item).Include(o => o.Order);

            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_ID = id;
            return View(orderDetails.ToList());
        }

        // GET: OrderManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", orderDetail.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderId", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailID,OrderID,ItemID,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();

                UpdateQuantity(db.Orders.Find(orderDetail.OrderID));
                return RedirectToAction("Details", new { id = orderDetail.OrderID });
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", orderDetail.ItemID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderID);
            return View(orderDetail);
        }

        private void UpdateQuantity(Order order)
        {
            order.Total = 0;
                foreach( var i in order.OrderDetails.ToList<OrderDetail>())
                {
                    order.Total += i.Quantity * i.UnitPrice;
                }
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
        }

        /* // GET: OrderManager/Edit/5
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
             return View(order);
         }

         // POST: OrderManager/Edit/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(Order order)
         {
             if (ModelState.IsValid)
             {
                 db.Entry(order).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             return View(order);
         }
         */
         // GET: OrderManager/Delete/5
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



        // POST: OrderManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
           // foreach(var i in order.OrderDetails.ToList<OrderDetail>())
            //{
             //   db.OrderDetails.Remove(i);
            //}
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: OrderDetails/Delete/5
        public ActionResult DeleteOrderDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("DeleteOrderDetail")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrderDetailConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            int order_id = orderDetail.OrderID;
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();

            UpdateQuantity(db.Orders.Find(order_id));
            return RedirectToAction("Details", new { id = order_id });
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

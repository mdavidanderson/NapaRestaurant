using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantWebSite.Models;

namespace RestaurantWebSite.Controllers
{   
    [Authorize]
    public class CheckoutController : Controller
    {
        RestaurantDB storeDB = new RestaurantDB();

        const string PromoCode = "FREE";
        bool isFree = false;
        string cardNumberCR="";
        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            isFree = false;
            cardNumberCR="";
            cardNumberCR = values["cardNumber"];
            TryUpdateModel(order);
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                StringComparison.OrdinalIgnoreCase) == true)
                {
                    isFree = true;
                    
                    order.Username = User.Identity.Name;
                    order.Email = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = OrderCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    return RedirectToAction("Complete",
                    new { id = order.OrderId });
                    
                }
                else if (string.IsNullOrEmpty(values["cardNumber"])==false)
                {
                    cardNumberCR = values["cardNumber"];
                    order.Username = User.Identity.Name;
                    order.Email = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order  
                    var cart = OrderCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    return RedirectToAction("CompleteWithCard",
                    new { id = order.OrderId });
                }
                else
                {
                    return View(order);
                }
            }

            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult CompleteWithCard(int id)
        {
            ViewBag.cardViewbag = cardNumberCR;
          
            bool isValid = storeDB.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);
           
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
            o => o.OrderId == id &&
            o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);  
            }
            else
            {
                return View("Error");
            }
        }
    }
}

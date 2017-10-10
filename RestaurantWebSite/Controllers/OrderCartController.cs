using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantWebSite.Models;
using RestaurantWebSite.ViewModels;

namespace RestaurantWebSite.Controllers
{
    public class OrderCartController : Controller
    {
        RestaurantDB storeDB = new RestaurantDB();
        // GET: OrderCart
        public ActionResult Index()
        {
            var cart = OrderCart.GetCart(this.HttpContext);
            var viewModel = new OrderCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }
        
        public ActionResult AddtoCart(int id)
        {
            var addedItem = storeDB.Items.Single(Item => Item.ItemID == id);

            var cart = OrderCart.GetCart(this.HttpContext);
            cart.AddToCart(addedItem);

            //stays on same page
            return Redirect(Request.UrlReferrer.ToString());


        }
        //
        //ajax: /OrderCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = OrderCart.GetCart(this.HttpContext);

            string itemName = storeDB.Carts.Single(item => item.RecordId == id).Item.ItemName;

            int itemCount = cart.RemoveFromCart(id);

            //confirmation
            var results = new OrderCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) + " has been removed from your order."
            };//skipping rest of details
            return Json(results);
        }

        //cartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = OrderCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

    }
}
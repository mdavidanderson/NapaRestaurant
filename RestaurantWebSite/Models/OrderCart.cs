using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RestaurantWebSite.Models
{
    public partial class OrderCart
    {
        RestaurantDB storeDB = new RestaurantDB();

        string OrderCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static OrderCart GetCart(HttpContextBase context)
        {
            var cart = new OrderCart();
            cart.OrderCartId = cart.GetCartId(context);
            return cart;
        }
        public static OrderCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Item item)
        {
            //get matching cart and item instances
            var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == OrderCartId && c.ItemId == item.ItemID);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = item.ItemID,
                    CartId = OrderCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                //if item does not exist in the car, then add one to quantity
                cartItem.Count++;
            }
            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = storeDB.Carts.Single(cart => cart.CartId == OrderCartId && cart.RecordId == id);
            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                //save changes
                storeDB.SaveChanges();
            }
            return itemCount;

        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(cart => cart.CartId == OrderCartId);
            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(cart => cart.CartId == OrderCartId).ToList();
        }
        public int GetCount()
        {
            //get count of each item in cart and sum up
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == OrderCartId
                          select (int?)cartItems.Count).Sum();
            //return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            //item price*count 
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == OrderCartId
                              select (int?)cartItems.Count * cartItems.Item.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            //iterate over the items in cart, adding order details to each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemID = item.ItemId,
                    OrderID = order.OrderId,
                    UnitPrice = item.Item.Price,
                    Quantity = item.Count
                };
                //set order total of cart
                orderTotal += (item.Count * item.Item.Price);

                storeDB.OrderDetails.Add(orderDetail);
            }
            //set order total to ordertotal count
            order.Total = orderTotal;
            storeDB.Entry(order).State = System.Data.Entity.EntityState.Modified;
            storeDB.SaveChanges();
            EmptyCart();
            return order.OrderId;
        }
        //using httpcontextbase to allow access to cookies
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if(!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();

                    //sent tempCardId back to clien as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        //when user logged in, bring cart to username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(c => c.CartId == OrderCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }

         
    }
}
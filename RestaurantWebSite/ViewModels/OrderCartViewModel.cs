using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantWebSite.Models;

namespace RestaurantWebSite.ViewModels
{
    public class OrderCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
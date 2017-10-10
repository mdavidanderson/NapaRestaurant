using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantWebSite.Models
{
    public class RestaurantDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RestaurantDB() : base("name=RestaurantDB")
        {
        }

        public System.Data.Entity.DbSet<RestaurantWebSite.Models.Item> Items { get; set; }
        public System.Data.Entity.DbSet<RestaurantWebSite.Models.Cart> Carts { get; set; }
        public System.Data.Entity.DbSet<RestaurantWebSite.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<RestaurantWebSite.Models.OrderDetail> OrderDetails { get; set; }

    }
}

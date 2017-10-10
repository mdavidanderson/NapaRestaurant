using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantWebSite.Models
{
    public class OrderDetail
    {
        
        public int OrderDetailID { get; set; }
        [Display(Name = "Order Name")]
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Dish")]
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace RestaurantWebSite.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        [Display(Name = "Dish")]
        public int ItemId { get; set; }
        [Display(Name = "Count")]
        public int Count { get; set; }
        [Display(Name = "Email")]
        public System.DateTime DateCreated { get; set; }
       
        public virtual Item Item { get; set;}
    }
}
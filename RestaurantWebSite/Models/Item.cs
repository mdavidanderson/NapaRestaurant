using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantWebSite.Models
{
    public class Item
    {
        public virtual int ItemID { get; set; }
        [Required]
        [Display(Name = "Dish Name")]
        public virtual string ItemName { get; set; }
        [Required]
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual decimal Price { get; set; }
        public virtual string ItemImgUrl { get; set; }
        [Required]
        [Display(Name = "Menu Category")]
        public virtual string ItemCategory { get; set; }//entre, side, beverage, desert
        [Required]
        [Display(Name = "Dish Description")]
        public virtual string ItemDescription { get; set; }

    }
}
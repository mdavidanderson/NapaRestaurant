using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantWebSite.ViewModels
{
    public class OrderCartRemoveViewModel
    {
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Total")]
        public decimal CartTotal { get; set; }
        [Display(Name = "Count")]
        public int CartCount { get; set; }
        [Display(Name = "Number of Dishes")]
        public int ItemCount { get; set; }
       
        public int Deleted { get; set; }
    }
}
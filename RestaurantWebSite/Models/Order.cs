using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RestaurantWebSite.Models
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {
        [Display(Name = "Order Number")]
        [ScaffoldColumn(false)]
        public virtual int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public virtual string Username { get; set;}

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Address")]
        public virtual string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "City")]
        public virtual string City { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "State")]
        public virtual string State { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Postal Code")]
        public virtual string PostalCode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Phone")]
        public virtual string Phone { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Email")]
        public virtual string Email { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual decimal Total { get; set; }

        [Display(Name = "Order Date" )]
        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "Order Details")]
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
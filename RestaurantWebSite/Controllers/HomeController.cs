using ContactUs.ViewModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace RestaurantWebSite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
           
            return View();
        }

        [AllowAnonymous]
        public ActionResult Employment()
        {

            return View();
        }

        [AllowAnonymous]
        public ActionResult ImagesSlideShow()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ContactUs()
        {
      
            return View();
        }
           [HttpPost]
        [AllowAnonymous]
        public ActionResult ContactUs(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("hugo_avalos17@hotmail.com");//Where mail will be sent 
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("TEST446C@gmail.com", "HELLOWORLD#");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }
        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }






    }


}
  


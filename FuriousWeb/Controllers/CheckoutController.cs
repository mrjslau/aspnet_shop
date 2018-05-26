using FuriousWeb.Data;
using FuriousWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FuriousWeb.Controllers
{
    public class CheckoutController : Controller
    {

        public ActionResult OpenCheckout()
        {
            Checkout checkout = new Checkout();
            return View("../Checkout", checkout);
        }

        public ActionResult SubmitPayment()
        {
            Checkout checkout = new Checkout()
            {
                Card_number = String.Format("{0}", Request.Form["card_number"]),
                Exp_year = Int32.Parse(Request.Form["exp_year"]),
                Exp_month = Int32.Parse(Request.Form["exp_month"]),
                Card_holder = String.Format("{0}", Request.Form["name"] + " " + Request.Form["lastname"]),
                Card_cvv = String.Format("{0}", Request.Form["card_cvv"])
            };
            if (checkout.InitPayment())
            {
                //return success messahe
                return View("../Thank-you");
            }
            else
            {
                //return error message
                //var errorMsg = checkout.paymentErr;
                return View("../Checkout", checkout);
            }
        }
    }
}
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
            return View("../Checkout");
        }

        public ActionResult SubmitPayment()
        {
            Checkout checkout = new Checkout();
            checkout.card_number = String.Format("{0}", Request.Form["card_number"]);
            checkout.exp_year = Int32.Parse(Request.Form["exp_year"]);
            checkout.exp_month = Int32.Parse(Request.Form["exp_month"]);
            checkout.card_holder = String.Format("{0}", Request.Form["name"] + " " + Request.Form["lastname"]);
            checkout.card_cvv = String.Format("{0}", Request.Form["card_cvv"]);
            if (checkout.initPayment())
            {
                //return success messahe
                return View("../Thank-you");
            }
            else
            {
                //return error message
                return View("../Checkout");
            }
        }
    }
}
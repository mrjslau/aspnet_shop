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

       /* public ActionResult FinalizeCheckout()
        {
            //Call payment API
            
        }*/

    }
}
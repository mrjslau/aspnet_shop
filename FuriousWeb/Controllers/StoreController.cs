using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuriousWeb.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            return View("Home/Index");
        }
    }
}
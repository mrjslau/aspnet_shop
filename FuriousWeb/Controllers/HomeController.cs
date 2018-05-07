using System.Web.Mvc;
using FuriousWeb.Models;

namespace FuriousWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowCrossSite]
        public ActionResult Cart()
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            ViewBag.Message = "Enter your payment details";

            ShoppingCart cart = new ShoppingCart();
            //cart.Add(1, 10);
            //cart.Add(2, 10);
            //cart.Add(3, 10);
            //ViewBag.Products = cart.Products;
            return View();
        }
    }
}
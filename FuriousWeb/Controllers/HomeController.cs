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
            Cart cart = new Cart();
            cart.Set("1","2");
            cart.Set("2","3");
            cart.Set("1","3");
            ViewBag.Products = cart.GetDictionary();
            return View();
        }
    }
}
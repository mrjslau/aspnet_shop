using FuriousWeb.Models;
using System.Web.Mvc;

namespace FuriousWeb.Controllers
{
    public class ShoppingCartController : Controller
    {

        public ActionResult OpenCart()
        {
            ShoppingCart shoppingCart = null;
            if (HttpContext.Session["shoppingCart"] != null)
            {
                shoppingCart = (ShoppingCart)HttpContext.Session["shoppingCart"];
            }
            else
            {
                shoppingCart = new ShoppingCart();
                HttpContext.Session["shoppingCart"] = shoppingCart;
            }

            return View("../ShoppingCart", shoppingCart.GetItems());
        }

        public ActionResult AddItem(int productId, long quantity)
        {
            ShoppingCart shoppingCart = null;
            if (HttpContext.Session["shoppingCart"] != null)
            {
                shoppingCart = (ShoppingCart)HttpContext.Session["shoppingCart"];
            }
            else
            {
                shoppingCart = new ShoppingCart();
                HttpContext.Session["shoppingCart"] = shoppingCart;
            }
            shoppingCart.Add(productId, quantity);
            
            long shoppingCartItemsCount = shoppingCart.CountItems();
            HttpContext.Session["shoppingCartItemsCount"] = shoppingCartItemsCount;

            return Json(new { success = true, shoppingCartItemsCount = shoppingCartItemsCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteItem(int productId)
        {
            var shoppingCart = (ShoppingCart)HttpContext.Session["shoppingCart"];
            shoppingCart.Remove(productId);

            long shoppingCartItemsCount = shoppingCart.CountItems();
            HttpContext.Session["shoppingCartItemsCount"] = shoppingCartItemsCount;

            return View("../ShoppingCart", shoppingCart.GetItems());
        }

        
    }
}
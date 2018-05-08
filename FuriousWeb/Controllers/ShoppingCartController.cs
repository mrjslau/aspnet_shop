using FuriousWeb.Data;
using FuriousWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                shoppingCart = (ShoppingCart)HttpContext.Session["shoppingCart"];
            else
                shoppingCart = new ShoppingCart();

            shoppingCart.Add(productId, quantity);
            HttpContext.Session["shoppingCart"] = shoppingCart;

            long shoppingCartItemsCount = shoppingCart.CountItems();
            HttpContext.Session["shoppingCartItemsCount"] = shoppingCartItemsCount;

            return Json(new { success = true, shoppingCartItemsCount = shoppingCartItemsCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteItem(int productId)
        {
            throw new NotImplementedException();
        }

        public ActionResult EditItemQuantity(int productId, long newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
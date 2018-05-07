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
            var shoppingCart = new ShoppingCart(); //TODO imti iš sesijos

            return View("../ShoppingCart", shoppingCart.GetItems());
        }

        public ActionResult AddItem(int productId)
        {
            throw new NotImplementedException();
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
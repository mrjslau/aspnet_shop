using FuriousWeb.Data;
using FuriousWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace FuriousWeb.Controllers
{
    public class CheckoutController : Controller
    {

        private DatabaseContext db = new DatabaseContext();

        public ActionResult OpenCheckout()
        {
            Checkout checkout = new Checkout();
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                checkout.User = user;
                return View("Checkout", checkout);
            }
            else
            {
                var url = this.Url.Action("OpenCheckout", "Checkout");
                TempData["redirectTo"] = url;
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult SubmitPayment()
        {
            var user = User.Identity.GetUserName();
            Checkout checkout = new Checkout()
            {
                Card_number = Request.Form["card_number"],
                Exp_year = Int32.Parse(Request.Form["exp_year"]),
                Exp_month = Int32.Parse(Request.Form["exp_month"]),
                Card_holder = user,
                Card_cvv = Request.Form["card_cvv"],

                User = User.Identity.GetUserId(),
            };
            
            if (checkout.InitPayment((ShoppingCart)HttpContext.Session["shoppingCart"]))
            {
                //save payment
                var payment = new Payment();
                payment.Amount = checkout.paymentInfo.Amount;
                payment.Created_at = checkout.paymentInfo.Created_at;
                payment.Code = checkout.paymentInfo.Id;
                db.Payments.Add(payment);

                //save order
                var order = new Order();
                order.PaymentID = payment.ID;
                order.UserID = checkout.User;
                var date = DateTime.Parse(checkout.paymentInfo.Created_at);
                order.Created_at = date.ToString("yyyy-MM-dd");
                order.Status = 1;
                db.Orders.Add(order);

                //save order details
                foreach(ShoppingCartItem item in checkout.Cart.GetItems())
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = order.ID;
                    orderDetail.ProductID = item.Product.Id;
                    orderDetail.Quantity = item.Quantity;
                    db.OrderDetails.Add(orderDetail);
                }
                db.SaveChanges();

                HttpContext.Session["shoppingCartItemsCount"] = 0;
                HttpContext.Session["shoppingCart"] = new ShoppingCart();

                return View("Thank-you");
            }
            else
            {
                return View("Checkout", checkout);
            }
        }
    }
}
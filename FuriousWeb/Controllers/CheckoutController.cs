using FuriousWeb.Data;
using FuriousWeb.Models;
using FuriousWeb.Models.ViewModels;
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
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();

                var viewModel = new CheckoutViewModel();
                viewModel.UserID = user;
                viewModel.User = db.Users.Find(user);

                return View("Checkout", viewModel);
            }
            else
            {
                var url = this.Url.Action("OpenCheckout", "Checkout");
                TempData["redirectTo"] = url;
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult SubmitPayment(CheckoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserName();
                Checkout checkout = new Checkout()
                {
                    Card_number = viewModel.Card_number,
                    Exp_year = viewModel.Exp_year,
                    Exp_month = viewModel.Exp_month,
                    Card_holder = user,
                    Card_cvv = viewModel.Card_cvv,

                    UserID = User.Identity.GetUserId(),
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
                    order.UserID = checkout.UserID;
                    var date = DateTime.Parse(checkout.paymentInfo.Created_at);
                    order.Created_at = date.ToString("yyyy-MM-dd");
                    order.Status = 1;
                    db.Orders.Add(order);

                    //save order details
                    foreach (ShoppingCartItem item in checkout.Cart.GetItems())
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.OrderID = order.ID;
                        orderDetail.ProductID = item.Product.Id;
                        orderDetail.Quantity = item.Quantity;
                        db.OrderDetails.Add(orderDetail);
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Error", ex.Message);
                        return View("Checkout", viewModel);
                    }
                    HttpContext.Session["shoppingCartItemsCount"] = 0;
                    HttpContext.Session["shoppingCart"] = new ShoppingCart();

                    return View("Thank-you");
                }
                else
                {
                    viewModel.paymentErr = checkout.paymentErr;
                    return View("Checkout", viewModel);
                }
            }

            //!ModelState.IsValid
            return View("Checkout", viewModel);
        }
    }
}
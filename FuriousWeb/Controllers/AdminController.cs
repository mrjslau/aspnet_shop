using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FuriousWeb.Data;
using FuriousWeb.Models;
using FuriousWeb.Models.ViewModels;
using System.Web.Security;
using System.Data.Entity;

namespace FuriousWeb.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDashboardForAdmin()
        {
            return View("DashboardForAdmin");
        }

        public int GetUsersCount(string query)
        {
            var users = db.Users.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users.Where(x => x.Email.ToLower().Contains(query.ToLower())).ToList();
            }
            else
            {
                users = db.Users.ToList();
            }
            int count = users.Count();
            return count;
        }

        public int GetOrdersCount(string query)
        {
            var orders = db.Orders.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                orders = orders.Where(order => order.User.Email.ToLower().Contains(query.ToLower()) || order.ID.Equals(query)).ToList();
            }
            else
            {
                orders = db.Orders.ToList();
            }
            int count = orders.Count();
            return count;
        }

        public ActionResult GetUsersListForAdmin(bool isPartial, string query, int currentPage)
        {
            int skip = (currentPage - 1) * 12;
            int take = 12;
            var users = db.Users.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users.Where(x => x.Email.ToLower().Contains(query.ToLower())).Skip(skip).Take(take).ToList();
            }
            ViewBag.date = DateTime.Now;
            if (isPartial)
                return PartialView("UsersForAdmin", users);
            else
                return View("UsersForAdmin", users);
        }

        public ActionResult GetOrderListForAdmin(bool isPartial, string query, int currentPage)
        {
            int skip = (currentPage - 1) * 12;
            int take = 12;
            var orders = db.Orders.ToList();
            if (!string.IsNullOrWhiteSpace(query))
                orders = orders.Where(order => order.User.Email.ToLower().Contains(query.ToLower()) || order.ID.Equals(query)).Skip(skip).Take(take).ToList();
            else
                orders = db.Orders.OrderBy(o => o.ID).Skip(skip).Take(take).ToList();
            if (isPartial)
                return PartialView("OrdersForAdmin", orders);
            else
                return View("OrdersForAdmin", orders);
        }

        public ActionResult EditOrder(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Order order = db.Orders.Find(id);
            if (order == null)
                return HttpNotFound();

            EditOrderViewModel viewModel = new EditOrderViewModel(order);
            
            return View("Orders/EditOrder", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(EditOrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Order order = db.Orders.Find(viewModel.Id);
                order.Status = viewModel.Status;

                db.Entry(order).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex);
                    return View(viewModel);
                }
                return RedirectToAction("GetOrderListForAdmin", "Admin", new { isPartial = false, query = "", currentPage = 1 });
            }
            return View("Orders/EditOrder", viewModel);
        }
    }
}
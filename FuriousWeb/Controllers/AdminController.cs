using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuriousWeb.Data;
using FuriousWeb.Models;
using FuriousWeb.Models.ViewModels;

namespace FuriousWeb.Controllers
{
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
    }
}
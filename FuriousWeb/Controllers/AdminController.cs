﻿using System;
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
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models.ViewModels
{

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Būtina įvesti pašto adresą")]
        [Display(Name = "E-paštas")]
        [EmailAddress(ErrorMessage = "Nekorektiškas pašto adresas")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Būtina įvesti slaptažodį")]
        [DataType(DataType.Password)]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Display(Name = "Prisiminti?")]
        public bool RememberMe { get; set; }
    }

    public class ProfileViewModel
    {
        public string UserID;
        public List<Order> Orders;
        public Order Order;
        public List<OrderDetail> OrderDetails;
        public User User;
        public enum OrderStatus
        {
            Apdorojama = 0,
            Įvykdyta = 1,
            Atšaukta = 2
        }

        public string GetStatus(int status)
        {
            return ((OrderStatus)status).ToString();
        }

        public double GetSum(double price, long quantity)
        {
            return price * quantity;
        }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Būtina įvesti pašto adresą")]
        [EmailAddress(ErrorMessage = "Nekorektiškas pašto adresas")]
        [Display(Name = "E-paštas")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Būtina įvesti slaptažodį")]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Pakartoti slaptažodį")]
        [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Būtina įvesti telefono numerį")]
        [Phone]
        [Display(Name = "Telefono numeris")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Būtina įvesti adresą")]
        [Display(Name = "Adresas")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Būtina įvesti vardą")]
        [Display(Name = "Vardas")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Būtina įvesti pavardę")]
        [Display(Name = "Pavardė")]
        public string Lastname { get; set; }
    }
}

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
        [EmailAddress(ErrorMessage = "Nekorėktiškas pašto adresas")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Būtina įvesti slaptažodį")]
        [DataType(DataType.Password)]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Display(Name = "Prisiminti?")]
        public bool RememberMe { get; set; }
    }

    public class HomeViewModel
    {
        public string User;
        public System.Linq.IQueryable<Order> Orders;
        public List<OrderDetails> OrderDetails;
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Būtina įvesti pašto adresą")]
        [EmailAddress(ErrorMessage = "Nekorėktiškas pašto adresas")]
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
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

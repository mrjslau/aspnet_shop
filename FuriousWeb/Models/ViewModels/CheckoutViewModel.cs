using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Būtina įvesti kortelės numerį")]
        public string Card_number { get; set; }
        [Required(ErrorMessage = "Būtina įvesti metus")]
        public int Exp_year { get; set; }
        [Required(ErrorMessage = "Būtina įvesti mėnesį")]
        public int Exp_month { get; set; }
        [Required(ErrorMessage = "Būtina įvesti kortelės cvv")]
        public string Card_cvv { get; set; }

        [Required(ErrorMessage = "Būtina įvesti vardą")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Būtina įvesti pavardę")]
        public string LastName { get; set; }

        public PaymentError paymentErr { get; set; }

        public string UserID { get; set; }
        public User User { get; set; }
    }
}
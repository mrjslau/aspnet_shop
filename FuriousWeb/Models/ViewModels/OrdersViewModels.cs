using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models.ViewModels
{
    public class EditOrderViewModel
    {
        [Display(Name = "Vartotojas")]
        public string User { get; set; }
        
        [Display(Name = "Mokėjimo ID")]
        public int PaymentID { get; set; }

        [Display(Name = "Statusas")]
        public int Status { get; set; }

        [Display(Name = "Sukurta")]
        public string Created_at { get; set; }


        public int Id { get; set; }

        public EditOrderViewModel(Order order)
        {
            Id = order.ID;
            User = order.User.Email;
            PaymentID = order.PaymentID;
            Status = order.Status;
            Created_at = order.Created_at;
        }
        

        public EditOrderViewModel() { }
    }
}

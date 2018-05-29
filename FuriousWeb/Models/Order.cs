using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuriousWeb.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int PaymentID { get; set; }
        public int Status { get; set; }
        public string Created_at { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("PaymentID")]
        public virtual Payment Payment { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models
{
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public long Quantity { get; set; }
    }
}
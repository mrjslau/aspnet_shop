using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuriousWeb.Models
{
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public long Quantity { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
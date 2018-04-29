using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuriousWeb.Models
{
    [Table("Stock")]
    public class ProductInStock
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int ProductId { get; set; } //Foreign key

        public int WarehouseId { get; set; } //Foreign key

        public long Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
    }
}
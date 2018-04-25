using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FuriousWeb.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public int ProductId { get; set; } //cia tikriausiai irgi reikes i ForeignKey pakeist

        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuriousWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Created_at { get; set; }

        public virtual List<ProductImage> Images { get; set; }
    }
}
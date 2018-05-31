using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuriousWeb.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        
        public int ProductId { get; set; }

        public string RelativePath { get; set; }

        public bool IsMainImage { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        public double Amount { get; set; }
        public string Created_at { get; set; }
        public string Code { get; set; }

    }
}
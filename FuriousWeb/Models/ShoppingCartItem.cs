using FuriousWeb.Data;

namespace FuriousWeb.Models
{
    public class ShoppingCartItem
    {
        public Product Product { get; set; }

        public long Quantity { get; set; }
    }
}
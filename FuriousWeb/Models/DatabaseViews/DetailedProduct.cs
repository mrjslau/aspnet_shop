namespace FuriousWeb.Models
{
    public class DetailedProduct
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WarehouseId { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}
namespace DSD605Ass2MVC.Models
{
    public class Stock
    {
        public Guid StockId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductType { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }


    }
}

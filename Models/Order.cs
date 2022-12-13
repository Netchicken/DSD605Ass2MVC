namespace DSD605Ass2MVC.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public Guid CustomerId { get; set; }
        public Guid StockId { get; set; }
        public Guid StaffId { get; set; }


        //navigation
        public Customer? Customer { get; set; }
        public Stock? Stock { get; set; }
        public Staff? Staff { get; set; }
    }
}

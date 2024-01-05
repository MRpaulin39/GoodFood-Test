namespace OrderService.Models
{
    public class OrderLine
    {
        public Guid IdOrderLine { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdOrder { get; set; }
        public int Quantity { get; set; } = 0;
    }
}

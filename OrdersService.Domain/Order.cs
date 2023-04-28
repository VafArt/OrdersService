namespace OrdersService.Domain
{
    public class Order 
    {
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();
    }
}

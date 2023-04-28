namespace OrdersService.Domain
{
    public sealed class OrderLine
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; } = new Order();
    }
}

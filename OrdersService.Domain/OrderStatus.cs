namespace OrdersService.Domain
{
    public enum OrderStatus
    {
        New,
        AwaitingPayment,
        Paid,
        SentForDelivery,
        Delivered,
        Completed
    }
}

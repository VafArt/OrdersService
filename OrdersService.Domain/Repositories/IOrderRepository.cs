namespace OrdersService.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdWithOrderLinesAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Order order);

        void Remove(Order order);
    }
}

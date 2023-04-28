using Microsoft.EntityFrameworkCore;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;

namespace OrdersService.Persistance.Repositories
{
    internal sealed class OrderRepository : IOrderRepository
    {
        private readonly OrdersServiceDbContext _dbContext;

        public OrderRepository(OrdersServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Order order)
        {
            _dbContext.Set<Order>().Add(order);
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Order>()
                .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
        }

        public async Task<Order?> GetByIdWithOrderLinesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Order>()
                .Include(order => order.Lines)
                .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
        }

        public void Remove(Order order)
        {
            _dbContext.Set<Order>().Remove(order);
        }
    }
}

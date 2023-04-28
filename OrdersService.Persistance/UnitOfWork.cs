using OrdersService.Domain.Repositories;

namespace OrdersService.Persistance
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly OrdersServiceDbContext _dbContext;

        public UnitOfWork(OrdersServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

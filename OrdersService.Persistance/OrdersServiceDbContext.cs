using Microsoft.EntityFrameworkCore;
using OrdersService.Domain;

namespace OrdersService.Persistance
{
    public class OrdersServiceDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public OrdersServiceDbContext(DbContextOptions<OrdersServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

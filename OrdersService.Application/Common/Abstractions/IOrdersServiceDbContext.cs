using Microsoft.EntityFrameworkCore;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.Abstractions
{
    public interface IOrdersServiceDbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

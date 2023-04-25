using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Persistance
{
    public class DbInitializer
    {
        public static void Initialize(OrdersServiceDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}

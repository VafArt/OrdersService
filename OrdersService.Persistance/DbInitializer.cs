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

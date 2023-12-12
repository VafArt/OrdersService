namespace OrdersService.Persistance
{
    public class DbInitializer
    {
        public static void Initialize(OrdersServiceDbContext context, OrdersServiceIdentityDbContext identityContext)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();

            identityContext.Database.EnsureDeleted();
            identityContext.Database.EnsureCreated();
            identityContext.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain;

namespace OrdersService.Persistance.EntityTypeConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.Status).HasConversion(status => status.ToString(), status => Enum.Parse<OrderStatus>(status));
            builder.HasKey(order => order.Id);
            builder.HasIndex(order => order.Id).IsUnique();
            builder.HasMany(order => order.Lines)
                .WithOne(orderLine => orderLine.Order)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

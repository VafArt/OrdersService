using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain;

namespace OrdersService.Persistance.EntityTypeConfigurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(orderLine => orderLine.Id);
            builder.HasIndex(orderLine => orderLine.OrderId).IsUnique();
            builder.HasIndex(orderLine => orderLine.ProductId).IsUnique();
        }
    }
}

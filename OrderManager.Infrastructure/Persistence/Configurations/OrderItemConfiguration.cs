using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Core.Entities;

namespace OrderManager.Infrastructure.Persistence.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems");

			builder
				.Property(oi => oi.Name)
				.IsRequired()
				.HasMaxLength(200);

			builder
				.Property(oi => oi.Quantity)
				.IsRequired();

			builder.Property(oi => oi.Price)
				.IsRequired()
				.HasColumnType("decimal(18,2)");

			builder.HasOne(oi => oi.Order)
				.WithMany(o => o.Items)
				.HasForeignKey(oi => oi.OrderId);
		}
	}
}

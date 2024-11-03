
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.Id);

			builder
			   .Property(x => x.Id)
			   .HasConversion(orderId => orderId.Value,
										 dbId => OrderId.Of(dbId));

			builder.HasOne<Customer>()
				   .WithMany()
				   .HasForeignKey(x => x.CoustomerId)
				   .IsRequired();

			builder.HasMany(x => x.OrderItems)
				   .WithOne()
				   .HasForeignKey(x => x.OrderId);

			builder.ComplexProperty(
				o => o.OrderName, nameBuilder =>
				{
					nameBuilder.Property(n => n.Value)
							   .HasColumnName(nameof(Order.OrderName))
							   .HasMaxLength(100)
							   .IsRequired();
				}
				);

			builder.ComplexProperty(
				o => o.ShippingAddress, addressBuilder =>
				{
					addressBuilder.Property(n => n.FirstName)
							      .HasMaxLength(50)
							      .IsRequired();

					addressBuilder.Property(n => n.LastName)
								  .HasMaxLength(50)
								  .IsRequired();

					addressBuilder.Property(n => n.EmailAddress)
								  .HasMaxLength(50);

					addressBuilder.Property(n => n.AddressLine)
								  .HasMaxLength(180)
								  .IsRequired();

					addressBuilder.Property(n => n.Country)
								  .HasMaxLength(50);

					addressBuilder.Property(n => n.State)
								  .HasMaxLength(50);

					addressBuilder.Property(n => n.ZipCode)
								  .HasMaxLength(5)
								  .IsRequired();


				}
				);

			builder.ComplexProperty(
				o => o.BillingAddress, addressBuilder =>
				{
					addressBuilder.Property(n => n.FirstName)
								  .HasMaxLength(50)
								  .IsRequired();

					addressBuilder.Property(n => n.LastName)
								  .HasMaxLength(50)
								  .IsRequired();

					addressBuilder.Property(n => n.EmailAddress)
								  .HasMaxLength(50);

					addressBuilder.Property(n => n.AddressLine)
								  .HasMaxLength(180)
								  .IsRequired();

					addressBuilder.Property(n => n.Country)
								  .HasMaxLength(50);

					addressBuilder.Property(n => n.State)
								  .HasMaxLength(50);

					addressBuilder.Property(n => n.ZipCode)
								  .HasMaxLength(5)
								  .IsRequired();


				}
				);

			builder.ComplexProperty(
				o => o.Payment, paymentBuilder =>
				{
					paymentBuilder.Property(p => p.CartName)
								  .HasMaxLength(50);

					paymentBuilder.Property(p => p.CartNumber)
								  .HasMaxLength(24)
								  .IsRequired();

					paymentBuilder.Property(p => p.Expiration)
								  .HasMaxLength(10);
					
					paymentBuilder.Property(p => p.CVV)
								  .HasMaxLength(3);

					paymentBuilder.Property(p => p.PaymentMethod);

				});

			builder.Property(p => p.Status)
						   .HasDefaultValue(OrderStatus.Draft)
						   .HasConversion(
							  s => s.ToString(),
							  dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
							);

			builder.Property(x => x.TotalPrice);
		}
	}
}

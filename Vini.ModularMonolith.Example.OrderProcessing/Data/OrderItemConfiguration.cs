using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vini.ModularMonolith.Example.OrderProcessing.Data;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.Property(x => x.Id)
      .ValueGeneratedNever();

    builder.Property(x => x.Description)
      .HasMaxLength(Constants.DESCRIPTION_MAXLENGTH)
      .IsRequired();
  }
}

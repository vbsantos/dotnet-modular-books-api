using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vini.ModularMonolith.Example.Users.Domain;

namespace Vini.ModularMonolith.Example.Users.Infrastructure.Data;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
  public void Configure(EntityTypeBuilder<CartItem> builder)
  {
    builder.Property(item => item.Id).ValueGeneratedNever();
  }
}

public class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
  public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
  {
    builder.ToTable(nameof(UserStreetAddress));
    builder.Property(address => address.Id).ValueGeneratedNever();
    builder.ComplexProperty(usa => usa.StreetAddress);
  }
}

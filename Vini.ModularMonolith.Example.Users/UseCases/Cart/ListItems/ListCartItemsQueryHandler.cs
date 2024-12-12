using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Users.CartEndpoints;
using Vini.ModularMonolith.Example.Users.Interfaces;

namespace Vini.ModularMonolith.Example.Users.UseCases.Cart.ListItems;

internal class ListCartItemsQueryHandler : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
{
  private readonly IApplicationUserRepository _userRepository;

  public ListCartItemsQueryHandler(IApplicationUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var cartItems = user.CartItems
      .Select(item => new CartItemDto(
        Id: item.Id,
        Description: item.Description,
        UnitPrice: item.UnitPrice,
        BookId: item.BookId,
        Quantity: item.Quantity
      ))
      .ToList();

    return cartItems;
  }
}

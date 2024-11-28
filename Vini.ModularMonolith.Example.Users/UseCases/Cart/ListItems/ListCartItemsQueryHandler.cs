using Ardalis.Result;
using MediatR;

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

    return user.CartItems
      .Select(item => new CartItemDto(
        Id: item.Id,
        Description: item.Description,
        UnitPrice: item.UnitPrice,
        BookId: item.BookId,
        Quantity: item.Quantity
      ))
      .ToList();
  }
}

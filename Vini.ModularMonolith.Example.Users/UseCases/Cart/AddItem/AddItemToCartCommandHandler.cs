using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Books.Contracts;

namespace Vini.ModularMonolith.Example.Users.UseCases.Cart.AddItem;

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository;
  private readonly IMediator _mediator;

  public AddItemToCartCommandHandler(IApplicationUserRepository userRepository, IMediator mediator)
  {
    _userRepository = userRepository;
    _mediator = mediator;
  }

  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var query = new BookDetailsQuery(request.BookId);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      return Result.NotFound();
    }

    var bookDetails = result.Value;

    var description = $"{bookDetails.Title} by {bookDetails.Author}";

    var newCartItem = new CartItem(
      request.BookId,
      description,
      request.Quantity,
      bookDetails.Price
    );

    user.AddItemToCart(newCartItem);

    await _userRepository.SaveChangesAsync();

    return Result.Success();
  }
}

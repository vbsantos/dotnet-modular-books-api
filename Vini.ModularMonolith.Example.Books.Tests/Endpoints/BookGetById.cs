using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Vini.ModularMonolith.Example.Books.BookEnpoints;

namespace Vini.ModularMonolith.Example.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture) : TestBase<Fixture>
{
  private readonly Fixture _fixture = fixture;

  [Theory]
  [InlineData("a89f6cd7-4693-457b-9009-02205dbbfe45", "The Fellowship of the Ring")]
  [InlineData("e4fa19bf-6981-4e50-a542-7c9b26e9ec31", "The Two Towers")]
  [InlineData("17c61e41-3953-42cd-8f88-d3f698869b35", "The Return of the Kink")]
  public async Task ReturnsExpectedBookGivenIdAsync(string validId, string expectedTitle)
  {
    var id = Guid.Parse(validId);
    var request = new GetBookByIdRequest { Id = id };
    var testResult = await _fixture.Client.GETAsync<GetById, GetBookByIdRequest, BookDto>(request);

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Title.Should().Be(expectedTitle);
  }
}

using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Vini.ModularMonolith.Example.Books.BookEnpoints;

namespace Vini.ModularMonolith.Example.Books.Tests.Endpoints;

public class BookList(Fixture fixture) : TestBase<Fixture>
{
  private readonly Fixture _fixture = fixture;

  [Fact]
  public async Task ReturnsThreeBooksAsync()
  {
    var testResult = await _fixture.Client.GETAsync<List, ListBooksResponse>();

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Books.Count.Should().Be(3);
  }
}

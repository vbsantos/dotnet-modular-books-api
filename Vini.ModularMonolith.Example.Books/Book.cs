﻿using Ardalis.GuardClauses;

namespace Vini.ModularMonolith.Example.Books;

public class Book
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public string Title { get; private set; } = string.Empty;
  public string Author { get; private set; } = string.Empty;
  public decimal Price { get; private set; }

  public Book(Guid id, string title, string author, decimal price)
  {
    Id = Guard.Against.Default(id);
    Title = Guard.Against.NullOrEmpty(title);
    Author = Guard.Against.NullOrEmpty(author);
    Price = Guard.Against.Negative(price);
  }

  public void UpdatePrice(decimal newPrice) => Price = Guard.Against.Negative(newPrice);
}

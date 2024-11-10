namespace Vini.ModularMonolith.Example.Books;

internal class BookService : IBookService
{
    public List<BookDto> ListBooks()
    {
        return [
            new BookDto(Guid.NewGuid(), "The Fellowship of the Ring", "J.R.R. Tolkien"),
            new BookDto(Guid.NewGuid(), "The Two Towers", "J.R.R. Tolkien"),
            new BookDto(Guid.NewGuid(), "The Return of the King", "J.R.R. Tolkien"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Eye of the World", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Great Hunt", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Dragon Reborn", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Shadow Rising", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Fires of Heaven", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: Lord of Chaos", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: A Crown of Swords", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Path of Daggers", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: Winter's Heart", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: Crossroads of Twilight", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: Knife of Dreams", "Robert Jordan"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: The Gathering Storm", "Robert Jordan and Brandon Sanderson"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: Towers of Midnight", "Robert Jordan and Brandon Sanderson"),
            new BookDto(Guid.NewGuid(), "Wheel Of Time: A Memory of Light", "Robert Jordan and Brandon Sanderson")
        ];
    }
}

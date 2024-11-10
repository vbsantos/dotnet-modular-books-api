namespace Vini.ModularMonolith.Example.Books;

internal class BookService : IBookService
{
    public List<BookDto> ListBooks()
    {
        return [
            new BookDto(Id: Guid.NewGuid(), Title: "The Fellowship of the Ring", Author: "J.R.R. Tolkien"),
            new BookDto(Id: Guid.NewGuid(), Title: "The Two Towers", Author: "J.R.R. Tolkien"),
            new BookDto(Id: Guid.NewGuid(), Title: "The Return of the King", Author: "J.R.R. Tolkien"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Eye of the World", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Great Hunt", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Dragon Reborn", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Shadow Rising", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Fires of Heaven", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: Lord of Chaos", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: A Crown of Swords", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Path of Daggers", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: Winter's Heart", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: Crossroads of Twilight", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: Knife of Dreams", Author: "Robert Jordan"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: The Gathering Storm", Author: "Robert Jordan and Brandon Sanderson"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: Towers of Midnight", Author: "Robert Jordan and Brandon Sanderson"),
            new BookDto(Id: Guid.NewGuid(), Title: "Wheel Of Time: A Memory of Light", Author: "Robert Jordan and Brandon Sanderson")
        ];
    }
}

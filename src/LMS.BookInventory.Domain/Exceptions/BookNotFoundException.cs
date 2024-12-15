namespace LMS.BookInventory.Domain.Exceptions;

public class BookNotFoundException : Exception
{
    public Guid BookId { get; init; }

    public BookNotFoundException() { }

    public BookNotFoundException(Guid bookId)
    {
        BookId = bookId;
    }


}

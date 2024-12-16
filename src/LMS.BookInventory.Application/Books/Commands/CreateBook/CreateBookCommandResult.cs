namespace LMS.BookInventory.Application.Books.Commands.CreateBook;


public class CreateBookCommandResult
{
    /// <summary>
    /// potential return statuses 
    /// </summary>
    public enum Status
    {
        BookCreated = 0,
        DuplicateIsbnDetected
    }

    /// <summary>
    /// Newly generated BookId
    /// </summary>
    public Guid? BookId { get; }

    /// <summary>
    /// Created book's ISBN
    /// </summary>
    public string? Isbn {  get; }

    /// <summary>
    /// Command result status
    /// </summary>
    public Status ResultStatus { get; }


    private CreateBookCommandResult(Guid? bookId, string? isbn, Status result)
    {
        BookId = BookId;
        Isbn = isbn;
        ResultStatus = result;
    }

    public static CreateBookCommandResult DuplicatedIsbnDetected(string isbn)
    {
        return new CreateBookCommandResult(null, isbn, Status.DuplicateIsbnDetected);
    }

    public static CreateBookCommandResult BookCreated(Guid bookId, string isbn)
    {
        return new CreateBookCommandResult(bookId, isbn, Status.BookCreated);
    }

}

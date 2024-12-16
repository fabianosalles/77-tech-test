namespace LMS.BookInventory.Application.Books.Queries.GeyById;

public record GetBookByIdQueryResult
{
    public Domain.Entities.Book? Data { get; }

    public Status Code { get; }

    /// <summary>
    /// potential return statuses 
    /// </summary>
    public enum Status
    {
        Success = 0,
        BookNotFound
    }

    public static GetBookByIdQueryResult NotFound()
    {
        return new GetBookByIdQueryResult(null, Status.BookNotFound);
    }

    public static GetBookByIdQueryResult Success(Domain.Entities.Book? data)
    {
        return new GetBookByIdQueryResult(data, Status.Success);
    }

    private GetBookByIdQueryResult(Domain.Entities.Book? data, Status code)
    {
        Code = code;
        Data = data;
    }
}

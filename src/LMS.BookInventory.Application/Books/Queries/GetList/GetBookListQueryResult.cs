namespace LMS.BookInventory.Application.Books.Queries.GetList;

public record GetBookListQueryResult
{
    public int Offset { get; }

    public int Limit { get; }
    
    public int Total { get; }

    public IEnumerable<Domain.Entities.Book> Books { get; } = new HashSet<Domain.Entities.Book>();

    public Status Code { get; }

    /// <summary>
    /// potential return statuses 
    /// </summary>
    public enum Status
    {
        Success = 0,        
    }

    public static GetBookListQueryResult Success(int offset, int limit, int total, 
        IEnumerable<Domain.Entities.Book> books)
    {
        return new GetBookListQueryResult(offset, limit, total, books, Status.Success); 
    }

    private GetBookListQueryResult(int offset, int limit, int total, 
        IEnumerable<Domain.Entities.Book> books, Status code)
    {
        Offset = offset; 
        Limit = limit; 
        Total = total; 
        Books = books;
        Code = code;
    }
}

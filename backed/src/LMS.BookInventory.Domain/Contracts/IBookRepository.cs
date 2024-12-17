using LMS.BookInventory.Domain.Entities;


namespace LMS.BookInventory.Domain.Contracts;

public interface IBookRepository
{
    Task<bool> Exists(Guid id);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
    Task<Book> CreateAsync(Book book, CancellationToken cancellationToken);
    Task UpdateAsync(Book book, CancellationToken cancellationToken);        
    Task UpdateAsync(Guid id, string? isbn, string? name, string? author, 
        ushort? edition, string? description, string? publiser,
        CancellationToken cancellationToken);    
    Task DeleteAsync(Book book, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetListAsync(int offset, int limit, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
}
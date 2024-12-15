using LMS.BookInventory.Domain.Entities;

namespace LMS.BookInventory.Domain.Contracts;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
    Task CreateAsync(Book book, CancellationToken cancellationToken);
    Task UpdateAsync(Book book, CancellationToken cancellationToken);
    Task DeleteAsync(Book book, CancellationToken cancellationToken);
}
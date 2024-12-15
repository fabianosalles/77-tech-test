using LMS.BookInventory.Domain.Contracts;
using LMS.BookInventory.Domain.Entities;

namespace LMS.BookInventory.Infra.Repositories;

/// <summary>
/// Implementation for IBookRepository
/// </summary>
public class BookRepository: IBookRepository
{
    public Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using AutoMapper;
using LMS.BookInventory.Domain.Contracts;
using LMS.BookInventory.Domain.Entities;
using LMS.BookInventory.Domain.Exceptions;
using LMS.BookInventory.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace LMS.BookInventory.Infra.Repositories;

/// <summary>
/// Implementation for IBookRepository
/// </summary>
public class BookRepository: IBookRepository
{
    private readonly IMapper _mapper;
    private readonly BookContext _dbContext;

    public BookRepository(BookContext bookContent, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = bookContent;
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Books.Where(x => x.Id == id && !x.Deleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is not null)
            return _mapper.Map<Book>(result);

        return null;
    }

    public async Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Books.Where(x => x.Isbn == isbn && !x.Deleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is not null)
            return _mapper.Map<Book>(result);

        return null;
    }

    public async Task<Book> CreateAsync(Book book, CancellationToken cancellationToken)
    {        
        var dbBook = _mapper.Map<Database.Models.Book>(book);
        if (dbBook.Id == Guid.Empty)
            dbBook.Id = Guid.NewGuid();

        dbBook.LastUpdatedDateTime = DateTime.UtcNow;

        _dbContext.Add(dbBook);

        await _dbContext.SaveChangesAsync();
        
        var createdBook = _mapper.Map<Book>(dbBook);        
        return createdBook;

    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        var dbBook = await _dbContext.Books.FindAsync(book.Id, cancellationToken);

        if (dbBook is null)
         throw new BookNotFoundException(book.Id);

        dbBook.Isbn = book.Isbn;
        dbBook.Name = book.Name;
        dbBook.Author = book.Author;        
        dbBook.Edition = book.Edition;

        if (book.Description is not null)
            dbBook.Description = book.Description;

        if (book.Publisher is not null)
            dbBook.Publisher = book.Publisher;

        dbBook.LastUpdatedDateTime = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Soft delete the book by marking deleted = true
    /// </summary>
    /// <param name="book"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BookNotFoundException"></exception>
    public async Task DeleteAsync(Book book, CancellationToken cancellationToken)
    {
        var dbBook = await _dbContext.Books.FindAsync(book.Id, cancellationToken);
        if (dbBook is null)
            throw new BookNotFoundException(book.Id);

        dbBook.Deleted = true;
        dbBook.LastUpdatedDateTime = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);       

    }
}
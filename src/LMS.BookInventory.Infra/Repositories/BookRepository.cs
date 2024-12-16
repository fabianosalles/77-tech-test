using AutoMapper;
using LMS.BookInventory.Domain.Contracts;
using LMS.BookInventory.Domain.Entities;
using LMS.BookInventory.Domain.Exceptions;
using LMS.BookInventory.Infra.Database;
using LMS.BookInventory.Infra.Database.Models;
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

    public async Task<Domain.Entities.Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Books.Where(x => x.Id == id && !x.Deleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is not null)
            return _mapper.Map<Domain.Entities.Book>(result);

        return null;
    }

    public async Task<Domain.Entities.Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Books.Where(x => x.Isbn == isbn && !x.Deleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is not null)
            return _mapper.Map<Domain.Entities.Book>(result);

        return null;
    }

    public async Task<Domain.Entities.Book> CreateAsync(Domain.Entities.Book book, CancellationToken cancellationToken)
    {        
        var dbBook = _mapper.Map<Database.Models.Book>(book);
        if (dbBook.Id == Guid.Empty)
        {
            dbBook.Id = Guid.NewGuid();
        }

        dbBook.LastUpdatedDateTime = DateTime.UtcNow;
        _dbContext.Add(dbBook);
        await _dbContext.SaveChangesAsync();
        
        var createdBook = _mapper.Map<Domain.Entities.Book>(dbBook);
        
        return createdBook;

    }

    public async Task UpdateAsync(Domain.Entities.Book book, CancellationToken cancellationToken)
    {
        var dbBook = await _dbContext.Books.FindAsync(book.Id, cancellationToken);
        if (dbBook is null)
            throw new BookNotFoundException(book.Id);

        dbBook.Isbn = book.Isbn;
        dbBook.Name = book.Name;
        dbBook.Author = book.Author;
        dbBook.Edition = book.Edition;
        dbBook.Description = book.Description;
        dbBook.Publisher = book.Publisher;
        dbBook.LastUpdatedDateTime = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Guid id, string? isbn, string? name, string? author,
        ushort? edition, string? description, string? publiser,
        CancellationToken cancellationToken)
    {
        var dbBook = await _dbContext.Books.FindAsync(id, cancellationToken);
        if (dbBook is null)
            throw new BookNotFoundException(id);

        /* required fields cannot be null */
        if (isbn is not null) dbBook.Isbn = isbn;
        if (name is not null) dbBook.Name = name;
        if (author is not null) dbBook.Author = author;
        if (edition is not null) dbBook.Edition = edition.Value;
        
        dbBook.Description = description;
        dbBook.Publisher = publiser;
        dbBook.LastUpdatedDateTime = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    

    /// <summary>
    /// Soft delete the book by marking deleted = true
    /// </summary>    
    /// <exception cref="BookNotFoundException"></exception>
    public async Task DeleteAsync(Domain.Entities.Book book, CancellationToken cancellationToken)
    {
        await DeleteAsync(book.Id, cancellationToken);
    }

    /// <summary>
    /// Soft delete the book by marking deleted = true
    /// </summary>
    /// <param name="id"></param>    
    /// <exception cref="BookNotFoundException"></exception>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbBook = await _dbContext.Books.FindAsync(id, cancellationToken);
        if (dbBook is null)
            throw new BookNotFoundException(id);

        dbBook.Deleted = true;
        dbBook.LastUpdatedDateTime = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _dbContext.Books.AnyAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Domain.Entities.Book>> GetListAsync(int offset, int limit, 
        CancellationToken cancellationToken)
    {
        var dbBooks = await _dbContext.Books
            .AsNoTracking()
            .Where(x => x.Deleted == false)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<Domain.Entities.Book>>(dbBooks);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Books
            .Where(x => x.Deleted == false)
            .CountAsync(cancellationToken);
    }

}
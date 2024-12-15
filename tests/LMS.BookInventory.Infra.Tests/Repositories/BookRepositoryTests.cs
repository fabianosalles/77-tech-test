using AutoMapper;
using LMS.BookInventory.Domain.Entities;
using LMS.BookInventory.Domain.Exceptions;
using LMS.BookInventory.Infra.Database;
using LMS.BookInventory.Infra.Database.Models;
using LMS.BookInventory.Infra.Repositories;
using LMS.BookInventory.Infra.UnitTests.Helpers;
using SQLitePCL;

namespace LMS.BookInventory.Infra.UnitTests.Repositories;

public class BookRepositoryTests
{
    private readonly BookContext _context;
    private readonly IMapper _mapper;

    public BookRepositoryTests()
    {
        _mapper = AutomapperLoader.LoadInfrastructure();
        _context = BookContextDBMock.Get();
    }

    [Fact]
    public async Task CreateAsync_ValidBook_ReturnsBook()
    {
        // arrange
        var expectedId = Guid.NewGuid();
        var expectedIsbn = "0000000000000";
        var expectedName = " Test Book Name";
        var expectedAuthor = "Test Author";
        var expectedDescription = "Test Book Description";
        var expectedEdition = (ushort)1;
        var expectedPublisher = "Test Publisher";
        
        var repo = new BookRepository(_context, _mapper);
        var domainBook = new Domain.Entities.Book()
        {
            Id = expectedId,
            Isbn = expectedIsbn,
            Name = expectedName,
            Author = expectedAuthor,
            Description = expectedDescription,
            Edition = expectedEdition,
            Publisher = expectedPublisher, 
        };

        //act
        var result = await repo.CreateAsync(domainBook, CancellationToken.None);

        //assert        
        Assert.Equal(expectedId, result.Id);
        Assert.Equal(expectedIsbn, result.Isbn);
        Assert.Equal(expectedName, result.Name);
        Assert.Equal(expectedAuthor, result.Author);
        Assert.Equal(expectedDescription, result.Description);
        Assert.Equal(expectedEdition, result.Edition);
        Assert.Equal(expectedPublisher, result.Publisher);
    }

    [Fact]
    public async Task GetAsync_NonExistingBook_ReturnsNull()
    {
        // arrange                
        var repo = new BookRepository(_context, _mapper);

        //act
        var resultById = await repo.GetByIdAsync(Guid.Empty, CancellationToken.None);
        var resultByIsBN = await repo.GetByIsbnAsync(string.Empty, CancellationToken.None);

        //assert
        Assert.Null(resultById);
        Assert.Null(resultByIsBN);
    }

    [Fact]
    public async Task GetByIdAsync_BookExists_ReturnsBook()
    {
        // arrange                
        var repo = new BookRepository(_context, _mapper);
        var expectedBook = GetNewBook(Guid.NewGuid(), "0000000000010");

        //act
        await repo.CreateAsync(expectedBook, CancellationToken.None);
        var returnedBook = await repo.GetByIdAsync(expectedBook.Id, CancellationToken.None);

        //assert
        Assert.NotNull(returnedBook);
        Assert.Equal(expectedBook.Id, returnedBook.Id);
        Assert.Equal(expectedBook.Isbn, returnedBook.Isbn);
        Assert.Equal(expectedBook.Name, returnedBook.Name);
        Assert.Equal(expectedBook.Author, returnedBook.Author);
        Assert.Equal(expectedBook.Description, returnedBook.Description);
        Assert.Equal(expectedBook.Edition, returnedBook.Edition);
        Assert.Equal(expectedBook.Publisher, returnedBook.Publisher);
    }

    [Fact]
    public async Task GetByIdAsync_BookDeleted_ReturnsNull()
    {
        // arrange                
        var repo = new BookRepository(_context, _mapper);
        var newBook = GetNewBook(Guid.NewGuid(), "0000000000011");

        //act
        await repo.CreateAsync(newBook, CancellationToken.None);
        await repo.DeleteAsync(newBook, CancellationToken.None);
        var returnedBook = await repo.GetByIdAsync(newBook.Id, CancellationToken.None);

        //assert
        Assert.Null(returnedBook);
    }

    [Fact]
    public async Task DeteleAsync_BookNonExistent_ThrowsBookNotFoundException()
    {
        // arrange                
        var repo = new BookRepository(_context, _mapper);
        var newBook = GetNewBook(Guid.NewGuid(), "0000000000011");

        //act              
        Func<Task> act = () => repo.DeleteAsync(newBook, CancellationToken.None);

        //assert
        await Assert.ThrowsAsync<BookNotFoundException>(() => act());
    }


    [Fact]
    public async Task UpdateAsync_BookNonExistent_ThrowsBookNotFoundException()
    {
        // arrange                
        var repo = new BookRepository(_context, _mapper);
        var newBook = GetNewBook(Guid.NewGuid(), "0000000000011");

        //act              
        Func<Task> act = () => repo.UpdateAsync(newBook, CancellationToken.None);

        //assert
        await Assert.ThrowsAsync<BookNotFoundException>(() => act());
    }


    #region 'Stub methods'

    private Domain.Entities.Book GetNewBook(Guid id, string? isb = null)
    {
        return new Domain.Entities.Book()
        {
            Id = id,
            Isbn = isb ?? "0000000000001",
            Name = "Book Name",
            Author = "Book Author",
            Description = "Book description",
            Edition = 1,
            Publisher = "Book Publisher",
        };
    }

    #endregion


}

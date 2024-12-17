using LMS.BookInventory.Domain.Entities;

namespace LMS.BookInventory.Domain.UnitTests.Entities;

public class BookTests
{
    [Fact]
    public void Equality_CompareTwoEqualBookObjectsUsingEqualMethod_ReturnsTrue()
    {
        // Arrange
        var guidID = new Guid("f0a76cc4-dba3-4180-9779-d0c766dcedb4");
        var author = "Some author";
        var isbn = "0000000000000";
        var name = "Some Name";
        var book1 = new Book {
            Id = guidID,      
            Author = author,
            Isbn = isbn,
            Name = name            
        };
        var book2 = new Domain.Entities.Book()
        {
            Id = guidID,
            Author = author,
            Isbn = isbn,
            Name = name
        };

        // Act
        var result = book1.Equals((object)book2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equality_CompareTheSameInstanceAgainstItselfUsingEqualMethod_ReturnsTrue()
    {
        // Arrange
        var guidID = new Guid("f0a76cc4-dba3-4180-9779-d0c766dcedb4");
        var author = "Some author";
        var isbn = "0000000000000";
        var name = "Some Name";
        var book1 = new Book
        {
            Id = guidID,
            Author = author,
            Isbn = isbn,
            Name = name
        };
        var book2 = book1;

        // Act
        var result = book1.Equals((object)book2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equality_CompareTwoNullBooksObjectsUsingOperator_ReturnsTrue()
    {
        // Arrange
        Book book1 = null!;
        Book book2 = null!;

        // Act
        var result = book1 == book2;

        // Assert
        Assert.True(result);
    }
}

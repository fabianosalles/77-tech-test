namespace LMS.BookInventory.Domain.Entities;

public sealed class Book : IEquatable<Book>
{
    public Guid Id { get; init; }

    /// <summary>  
    ///  <see href="https://en.wikipedia.org/wiki/ISBN">ISBN</see> 
    /// </summary>
    public required string Isbn { get; init; }

    public required string Name { get; init; }

    public required string Author { get; init; }

    public ushort Edition { get; init; }

    public string? Description { get; init; }
    
    public string? Publisher { get; init; }
      
        
    #region 'Equalty overrides'
    
    public bool Equals(Book? other)
    {
        return other != null && (
            Id == other.Id &&
            Isbn == other.Isbn &&
            Name == other.Name &&
            Author == other.Author &&
            Edition == other.Edition &&
            Description == other.Description &&
            Publisher == other.Publisher);
    }
    
    /// <summary>
    /// Custom equality implementation for Book.
    /// This will compare the books for any intersection.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override bool Equals(object? other)
    {
        if (ReferenceEquals(this, other))
            return true;

        if (other is Book book)
            return Equals(book);

        return false;
    }

    public static bool operator ==(Book? left, Book? right)
    {
        if (left is null)
            return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Book? left, Book? right) => !(left == right);

    public override int GetHashCode() =>
        (Id, Isbn, Name, Author, Edition, Description, Publisher).GetHashCode();
    
#endregion

}
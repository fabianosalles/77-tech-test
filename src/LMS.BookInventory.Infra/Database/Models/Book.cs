namespace LMS.BookInventory.Infra.Database.Models;

/// <summary>
/// Table designed to store Book state
/// </summary>
public class Book : IBaseTable<Guid>
{
    public Guid Id { get; set; }
    
    public required string Isbn { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public ushort Edition { get; set; }
    
    public string? Description { get; set; }

    public string? Publisher { get; set; }
    
    public bool Deleted { get; set; }

    public DateTimeOffset LastUpdatedDateTime { get; set; }

    public byte[] RowVersion { get; set; } = [];
}
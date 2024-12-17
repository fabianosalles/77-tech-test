namespace LMS.BookInventory.Infra.Database.Models;

/// <summary>
/// Generic interface for standard table fields
/// </summary>
public interface IBaseTable<T>
{
    /// <summary>
    /// Gets or sets the primary key
    /// </summary>
    /// <value></value>
    T Id { get; set; }

    /// <summary>
    /// Gets or sets the row version of the table
    /// </summary>
    /// <value></value>
    byte[] RowVersion { get; set; }
}
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using LMS.BookInventory.Infra.Database;

namespace LMS.BookInventory.Infra.UnitTests.Helpers;

/// <summary>
/// Mock database for BookContext. Uses SQLite in-memory database under the hood
/// </summary>
public static class BookContextDBMock
{
    public static BookContext Get()
    {
        // create a new sqlite connection, then open it
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        // grab sqlite db options and use it to create a new db context
        var options = new DbContextOptionsBuilder<BookContext>().UseSqlite(connection).Options;
        var context = new BookContext(options);
        var sql = context.Database.GenerateCreateScript();        
        context.Database.ExecuteSqlRaw(sql);

        return context;
    }
}

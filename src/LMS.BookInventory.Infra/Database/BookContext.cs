using LMS.BookInventory.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LMS.BookInventory.Infra.Database;
    
public class BookContext: DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options){}

    public virtual DbSet<Book> Books { get; set; }
}

/*
public class BookContextDesignFactory : IDesignTimeDbContextFactory<BookContext>
{
    public BookContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookContext>();
        optionsBuilder.UseSqlite("Data Source=blog.db");

        return new BookContext(optionsBuilder.Options);
    }
}*/
using LMS.BookInventory.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LMS.BookInventory.Infra.Database;
    
public class BookContext: DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options){}

    public virtual DbSet<Book> Books { get; set; }
}
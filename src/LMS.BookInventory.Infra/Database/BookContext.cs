using LMS.BookInventory.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.BookInventory.Infra.Database;
    
public class BookContext: DbContext
{
    public virtual DbSet<Book> Books { get; set; }
}
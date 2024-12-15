using LMS.BookInventory.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.BookInventory.Infra.Database.Configuration;

/// <summary>
/// Database configuration for Books table
/// </summary>
public class BooksConfiguration: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Isbn).IsUnique();
        builder.Property(x => x.Isbn)
            .HasMaxLength(13)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.Author)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Edition)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1024);
        
        builder.Property(x => x.Publisher)
            .HasMaxLength(255);
        
        builder.Property(x => x.RowVersion)
            .IsRowVersion();

    }
}
using System.ComponentModel.DataAnnotations;
using MediatR;


namespace LMS.BookInventory.Application.Books.Commands.CreateBook;

public record CreateBookCommand : IValidatableObject, IRequest<CreateBookCommandResult>
{
    [Required, MinLength(10), MaxLength(13)]
    public required string Isbn { get; init; }

    [Required, MaxLength(255)]
    public required string Name { get; init; }

    [Required, MaxLength(255)]
    public required string Author { get; init; }

    [Required]
    public ushort Edition { get; init; }

    [MaxLength(1024)]
    public string? Description { get; init; }

    [MaxLength(255)]
    public string? Publisher { get; init; }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        /* naive validation for ISBN numbers */        
        var _isbn = Isbn.Trim();
        var justNumbers = new String(_isbn.Where(Char.IsDigit).ToArray());
        if (justNumbers.Length != _isbn.Length)
        {
            yield return new ValidationResult("ISBN must have 10 or 13 digits");
        }
    }
}

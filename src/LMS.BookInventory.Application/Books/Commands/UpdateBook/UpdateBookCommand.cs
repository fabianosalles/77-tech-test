using LMS.BookInventory.Domain.Exceptions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LMS.BookInventory.Application.Books.Commands.UpdateBook;

public class UpdateBookCommand : IValidatableObject, IRequest<UpdateBookCommandResult>
{
    public required Guid Id { get; init; }

    /// <summary>
    /// Values to be updates
    /// </summary>
    public required UpdateBookCommandItem Values {  get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Id == Guid.Empty)
            yield return new ValidationResult("Id is a invalid GUID");
    }
}

[Serializable]
public class UpdateBookCommandItem
{
    [MinLength(10), MaxLength(13)]
    public string? Isbn { get; init; }

    [MaxLength(255)]
    public string? Name { get; init; }

    [Required, MaxLength(255)]
    public string? Author { get; init; }

    public ushort? Edition { get; init; }

    [MaxLength(1024)]
    public string? Description { get; init; }

    [MaxLength(255)]
    public string? Publisher { get; init; }
}


public record UpdateBookCommandResult
{
    /// <summary>
    /// potential return statuses 
    /// </summary>
    public enum Status
    {
        BookUpdated = 0,
        BookNotFound
    }
        
    public Status Code { get; }

    public static UpdateBookCommandResult BookNotFound()
        => new UpdateBookCommandResult(Status.BookNotFound);    

    public static UpdateBookCommandResult BookUpdated()
        => new UpdateBookCommandResult(Status.BookUpdated);    

    private UpdateBookCommandResult(Status code)
    {
        Code = code;
    }
}


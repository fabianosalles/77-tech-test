using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace LMS.BookInventory.Application.Books.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<DeleteBookCommandResult>
{
    public required Guid Id { get; init; }
}


public record DeleteBookCommandResult
{
    public Status Code { get; }

    public enum Status
    {
        Deleted = 0,
        BookNotFound
    }

    public static DeleteBookCommandResult Deleted()
        => new DeleteBookCommandResult(Status.Deleted);

    public static DeleteBookCommandResult NotFound()
        => new DeleteBookCommandResult(Status.BookNotFound);

    private DeleteBookCommandResult(Status code)
    {
        this.Code = code;
    }
}

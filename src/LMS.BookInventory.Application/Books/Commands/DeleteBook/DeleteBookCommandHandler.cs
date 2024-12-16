using LMS.BookInventory.Domain.Contracts;
using LMS.BookInventory.Domain.Exceptions;
using MediatR;

namespace LMS.BookInventory.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler
    : IRequestHandler<DeleteBookCommand, DeleteBookCommandResult>
{
    private IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<DeleteBookCommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _bookRepository.DeleteAsync(request.Id, cancellationToken);
        }
        catch (BookNotFoundException)
        {
            return DeleteBookCommandResult.NotFound();
        }

        return DeleteBookCommandResult.Deleted();
    }
}

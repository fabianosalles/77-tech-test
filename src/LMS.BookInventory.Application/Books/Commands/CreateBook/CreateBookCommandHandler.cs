using LMS.BookInventory.Domain.Contracts;
using MediatR;

namespace LMS.BookInventory.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookCommandResult>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<CreateBookCommandResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var existingBook = await _bookRepository.GetByIsbnAsync(request.Isbn, cancellationToken);
        
        if (existingBook is not null) 
            return CreateBookCommandResult.DuplicatedIsbnDetected(request.Isbn);

        var newBook = new Domain.Entities.Book()
        {
            Id = Guid.NewGuid(),
            Isbn = request.Isbn,
            Name = request.Name,
            Author = request.Author,
            Edition = request.Edition,  
            Description = request.Description,  
            Publisher = request.Publisher  
        };

        var result = await _bookRepository.CreateAsync(newBook, cancellationToken);

        return CreateBookCommandResult.BookCreated(result.Id, result.Isbn);
    }
}

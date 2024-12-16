using LMS.BookInventory.Domain.Contracts;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LMS.BookInventory.Application.Books.Queries.GeyById;

public class GetBookByIdQuery: IRequest<GetBookByIdQueryResult>
{
    [Required]
    public required Guid Id { get; init; }
}

public class GetBookByIdQueryHandler
    : IRequestHandler<GetBookByIdQuery, GetBookByIdQueryResult>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<GetBookByIdQueryResult> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(query.Id, cancellationToken);
        if (book is null)
        {
            return GetBookByIdQueryResult.NotFound();
        }

        return GetBookByIdQueryResult.Success(book);
    }
}



using LMS.BookInventory.Domain.Contracts;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LMS.BookInventory.Application.Books.Queries.GetList;

public class GetBookListQuery :IRequest<GetBookListQueryResult>
{
    [Range(0, int.MaxValue), Required]    
    public required int Offset { get; init; }
        
    [Range(1, 100), Required]    
    public required int Limit { get; init; }
}

public class GetBookListQueryhandler : IRequestHandler<GetBookListQuery, GetBookListQueryResult>
{
    private readonly IBookRepository _bookRepository;

    public GetBookListQueryhandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<GetBookListQueryResult> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        var bookList = await _bookRepository.GetListAsync(request.Offset, request.Limit, cancellationToken);
        var total = await _bookRepository.CountAsync(cancellationToken);
        return GetBookListQueryResult.Success(request.Offset, request.Limit, total, bookList);
    }
}

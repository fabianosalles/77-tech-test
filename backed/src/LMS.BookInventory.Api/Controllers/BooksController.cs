using LMS.BookInventory.Application.Books.Commands.CreateBook;
using LMS.BookInventory.Application.Books.Commands.DeleteBook;
using LMS.BookInventory.Application.Books.Commands.UpdateBook;
using LMS.BookInventory.Application.Books.Queries.GetList;
using LMS.BookInventory.Application.Books.Queries.GeyById;
using LMS.BookInventory.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BookInventory.Controllers;

/// <summary>
/// Controller using the Mediator pattern to dispatch commands to application layer
/// and map the results to http response codes
/// </summary>
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/books")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateBookCommandResult>> PostAsync(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        var status = result?.Code;
        return status switch
        {
            CreateBookCommandResult.Status.BookCreated => Created(HttpContext.Request.Path, result),

            CreateBookCommandResult.Status.DuplicateIsbnDetected => Conflict(new
            {
                isbn = result?.Isbn,
                message = "Duplicated ISBN detected"
            }),

            _ => throw new NotSupportedException($"The following result is not supported. Result: {status}")
        };
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateBookCommandResult>> PutAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateBookCommandItem values,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateBookCommand() { Id = id, Values = values }, cancellationToken);
        var status = result?.Code;
        return status switch
        {
            UpdateBookCommandResult.Status.BookNotFound => NotFound(result),
            UpdateBookCommandResult.Status.BookUpdated => NoContent(),
            _ => throw new NotSupportedException($"The following result is not supported. Result: {status}")
        };
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetBookListQueryResult>> GetAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookByIdQuery() { Id = id }, cancellationToken);
        var status = result?.Code;
        return status switch
        {
            GetBookByIdQueryResult.Status.Success => Ok(result),
            GetBookByIdQueryResult.Status.BookNotFound => NotFound(),
            _ => throw new NotSupportedException($"The following result is not supported. Result: {status}")
        };
    }
    

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetBookListQueryResult>> GetListAsync(
        [FromQuery] GetBookListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        var status = result?.Code;
        return status switch
        {
            GetBookListQueryResult.Status => Ok(result),
            _ => throw new NotSupportedException($"The following result is not supported. Result: {status}")
        };
    }



    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteBookCommandResult>> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteBookCommand() { Id = id }, cancellationToken);
        var status = result?.Code;
        return status switch
        {
            DeleteBookCommandResult.Status.Deleted => NoContent(),
            DeleteBookCommandResult.Status.BookNotFound => NotFound(),
            _ => throw new NotSupportedException($"The following result is not supported. Result: {status}")
        };
    }

}
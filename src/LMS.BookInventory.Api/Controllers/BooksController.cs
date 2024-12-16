using LMS.BookInventory.Application.Books.Commands.CreateBook;
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
        var status = result?.ResultStatus;

        return status switch
        {
            CreateBookCommandResult.Status.BookCreated => Created(HttpContext.Request.Path, result),
            CreateBookCommandResult.Status.DuplicateIsbnDetected => Conflict(result),
            _ => throw new NotSupportedException($"The following result is not supported. Result: {status}")
        };

    }
}
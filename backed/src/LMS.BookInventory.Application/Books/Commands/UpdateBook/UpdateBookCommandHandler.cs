﻿using AutoMapper;
using LMS.BookInventory.Domain.Contracts;
using LMS.BookInventory.Domain.Exceptions;
using MediatR;

namespace LMS.BookInventory.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookCommandResult>
{
    private IBookRepository _bookRepository;    

    public UpdateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;        
    }
    public async Task<UpdateBookCommandResult> Handle(
        UpdateBookCommand request, 
        CancellationToken cancellationToken)
    {        
        try
        {
            await _bookRepository.UpdateAsync(request.Id,
                request.Values.Isbn,
                request.Values.Name,                
                request.Values.Author,
                request.Values.Edition,
                request.Values.Description,
                request.Values.Publisher, 
                cancellationToken);
        }
        catch (BookNotFoundException)
        {
            return UpdateBookCommandResult.BookNotFound();
        }
        return UpdateBookCommandResult.BookUpdated();        
    }
}

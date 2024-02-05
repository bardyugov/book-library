using BookLibrary.Application.Commands.Books.Create;
using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Books.Create;

public class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<Book>>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookHandler(IBookRepository repository)
    {
        _bookRepository = repository;
    }
    
    public async Task<Result<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var isFindBook = await _bookRepository.FindByName(request.Name, cancellationToken);
        if (isFindBook.IsFailed)
            return isFindBook;

        return Result.Ok(new Book());
    }
}
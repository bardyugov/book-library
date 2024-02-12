using BookLibrary.Application.Queries.Books;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Books.GetBookByName;

public class GetBookByNameHandler : IRequestHandler<GetBookByNameQuery, Result<Book>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public GetBookByNameHandler(IAuthenticationService authenticationService, IBookRepository bookRepository)
    {
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<Book>> Handle(GetBookByNameQuery request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var isFindBook = await _bookRepository.FindByNameWithRelations(request.Name, cancellationToken);
        if (isFindBook.IsFailed)
            return isFindBook;

        if (author.Id != isFindBook.Value.Author.Id)
        {
            isFindBook.Value.CountReaders += 1;
            await _bookRepository.UpdateCountReaders(isFindBook.Value.Id, isFindBook.Value.CountReaders,
                cancellationToken);
        }

        return Result.Ok(isFindBook.Value);
    }
}
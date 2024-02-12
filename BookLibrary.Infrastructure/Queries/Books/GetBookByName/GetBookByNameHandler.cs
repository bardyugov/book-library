using BookLibrary.Application.Queries.Books.GetBookByNameQuery;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Books.GetBookByName;

public class GetBookByNameHandler : IRequestHandler<GetBookByNameQuery, Result<GetBookByNameQueryResult>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public GetBookByNameHandler(IAuthenticationService authenticationService, IBookRepository bookRepository)
    {
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }

    private GetBookByNameQueryResult MapToResult(Book book)
    {
        return new GetBookByNameQueryResult()
        {
            Name = book.Name,
            Description = book.Description,
            Path = book.Path,
            CountReaders = book.CountReaders,
            AuthorName = book.Author.Name,
            Comments = book.Comments,
            Complaints = book.Complaints,
            Opinions = book.Opinions
        };
    }
    
    public async Task<Result<GetBookByNameQueryResult>> Handle(GetBookByNameQuery request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var isFindBook = await _bookRepository.FindByNameWithRelations(request.Name, cancellationToken);
        if (isFindBook.IsFailed)
            return Result.Fail(isFindBook.Errors.Last().Message);

        if (author.Id != isFindBook.Value.Author.Id)
        {
            isFindBook.Value.CountReaders += 1;
            await _bookRepository.UpdateCountReaders(isFindBook.Value.Id, isFindBook.Value.CountReaders,
                cancellationToken);
        }

        var result = MapToResult(isFindBook.Value);
        
        return Result.Ok(result);
    }
}
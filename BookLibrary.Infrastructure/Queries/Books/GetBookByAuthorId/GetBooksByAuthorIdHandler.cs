using BookLibrary.Application.Queries.Books;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Books.GetBookByAuthorId;

public class GetBooksByAuthorIdHandler : IRequestHandler<GetBooksByAuthorIdQuery, List<Book>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public GetBooksByAuthorIdHandler(IAuthenticationService authenticationService, IBookRepository bookRepository)
    {
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }
    
    public async Task<List<Book>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var books = _bookRepository.FindManyWithAuthorId(author.Id, cancellationToken);

        return books;
    }
}
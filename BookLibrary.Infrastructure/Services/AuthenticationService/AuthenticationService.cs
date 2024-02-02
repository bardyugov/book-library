using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Infrastructure.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthenticationService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    
    public async Task<Author> Authenticate(HttpContext context, CancellationToken cancellationToken)
    {
        var stringAuthorId = context.User.Claims
            .ToList()
            .Find(x => x.Type == "AuthorId" )
            .Value;
        var id = Guid.Parse(stringAuthorId);
        
        var isAuthor = await _authorRepository.FindUnique(id, cancellationToken);
        if (isAuthor.IsFailed)
            throw new UnauthorizedAccessException("Authentication not available");

        return isAuthor.Value;
    }
}
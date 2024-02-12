using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Infrastructure.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IAuthorRepository authorRepository, IHttpContextAccessor httpContextAccessor)
    {
        _authorRepository = authorRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<Author> Authenticate(CancellationToken cancellationToken)
    {
        var stringAuthorId = _httpContextAccessor.HttpContext.User.Claims
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
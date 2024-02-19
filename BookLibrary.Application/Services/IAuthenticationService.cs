using BookLibrary.Domain.Models;

namespace BookLibrary.Application.Services;

public interface IAuthenticationService
{
    Task<Author> Authenticate(CancellationToken cancellationToken);
}
using BookLibrary.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Application.Services;

public interface IAuthenticationService
{
    Task<Author> Authenticate(HttpContext context, CancellationToken cancellationToken);
}
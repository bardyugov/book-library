using BookLibrary.Domain.Models;

namespace BookLibrary.Application.Services;

public interface IJwtService
{
    string GenerateToken(Author author);
}
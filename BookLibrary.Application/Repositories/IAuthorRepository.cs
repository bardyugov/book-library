using BookLibrary.Domain.Models;
using FluentResults;

namespace BookLibrary.Application.Repositories;

public interface IAuthorRepository
{
    Task Create(Author author, CancellationToken token);
    
    Task Remove(Guid id, CancellationToken token);
    
    Task<Result<Author>> FindUnique(Guid id, CancellationToken token);
    
    Task<List<Author>> FindAll(CancellationToken token);
}
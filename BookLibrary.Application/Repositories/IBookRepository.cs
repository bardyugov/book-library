using BookLibrary.Domain.Models;
using FluentResults;

namespace BookLibrary.Application.Repositories;

public interface IBookRepository : IBaseRepository
{
    Task Create(Book book, CancellationToken token);
    Task<Result<Book>> FindByName(string name, CancellationToken token);
    Task<Result<Book>> Remove(Guid id, CancellationToken token);

    Task<Result<Book>> FindById(Guid id, CancellationToken token);
}
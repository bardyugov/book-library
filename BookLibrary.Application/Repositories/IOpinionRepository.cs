using BookLibrary.Domain.Models;
using FluentResults;

namespace BookLibrary.Application.Repositories;

public interface IOpinionRepository : IBaseRepository
{
    Task Create(Opinion opinion, CancellationToken token);

    Task<Result> Remove(Guid id, CancellationToken token);

    Task<List<Opinion>> FindByAuthorId(Guid authorId, CancellationToken token);

    Task<Result<Opinion>> FindById(Guid id, CancellationToken token);
}
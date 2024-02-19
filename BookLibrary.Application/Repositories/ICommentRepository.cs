using BookLibrary.Domain.Models;
using FluentResults;

namespace BookLibrary.Application.Repositories;

public interface ICommentRepository : IBaseRepository
{
    Task Create(Comment comment, CancellationToken token);

    Task<Result> Remove(Guid id, CancellationToken token);

    Task<List<Comment>> FindByAuthorId(Guid authorId, CancellationToken token);

    Task<Result<Comment>> FindById(Guid id, CancellationToken token);

}
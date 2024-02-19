using BookLibrary.Domain.Models;
using FluentResults;

namespace BookLibrary.Application.Repositories;

public interface IComplaintRepository : IBaseRepository
{
    Task Create(Complaint opinion, CancellationToken token);

    Task<Result> Remove(Guid id, CancellationToken token);

    Task<List<Complaint>> FindByAuthorId(Guid authorId, CancellationToken token);

    Task<List<Complaint>> FindByBookId(Guid bookId, CancellationToken token);

    Task<Result<Complaint>> FindById(Guid id, CancellationToken token); 
}
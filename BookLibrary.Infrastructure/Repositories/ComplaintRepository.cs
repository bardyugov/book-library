using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class ComplaintRepository : IComplaintRepository
{
    private readonly DatabaseContext _databaseContext;

    public ComplaintRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Create(Complaint opinion, CancellationToken token)
    {
        await _databaseContext.Complaints.AddAsync(opinion, token);
    }

    public async Task<Result<Complaint>> FindById(Guid id, CancellationToken token)
    {
        var complaint = await _databaseContext.Complaints.FirstOrDefaultAsync(v => v.Id == id, token);
        if (complaint is null)
            return Result.Fail("Not found complaint");

        return Result.Ok(complaint);
    }

    public async Task<List<Complaint>> FindByAuthorId(Guid authorId, CancellationToken token)
    {
        var complaints = await _databaseContext.Complaints
            .Where(v => v.Author.Id == authorId)
            .ToListAsync(token);

        return complaints;
    }

    public async Task<Result> Remove(Guid id, CancellationToken token)
    {
        var isComplaint = await FindById(id, token);
        if (isComplaint.IsFailed)
            return Result.Fail(isComplaint.Errors.Last().Message);

        _databaseContext.Complaints.Remove(isComplaint.Value);
        return Result.Ok();
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _databaseContext.SaveChangesAsync(token);
    }

    public async Task<List<Complaint>> FindByBookId(Guid bookId, CancellationToken token)
    {
        var complaints = await _databaseContext.Complaints
            .Where(v => v.Book.Id == bookId)
            .ToListAsync(token);

        return complaints;
    }
}
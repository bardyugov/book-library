using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class OpinionRepository : IOpinionRepository
{
    private readonly DatabaseContext _databaseContext;

    public OpinionRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Create(Opinion opinion, CancellationToken token)
    {
        await _databaseContext.Opinions.AddAsync(opinion, token);
    }

    public async Task<Result<Opinion>> FindById(Guid id, CancellationToken token)
    {
        var opinion = await _databaseContext.Opinions.FirstOrDefaultAsync(v => v.Id == id, token);
        if (opinion is null)
            return Result.Fail("Not found opinions");

        return opinion;
    }

    public async Task<Result> Remove(Guid id, CancellationToken token)
    {
        var isFindOpinion = await FindById(id, token);
        if (isFindOpinion.IsFailed)
            return Result.Fail(isFindOpinion.Errors.Last().Message);

        _databaseContext.Opinions.Remove(isFindOpinion.Value);

        return Result.Ok();
    }

    public async Task<List<Opinion>> FindByAuthorId(Guid authorId, CancellationToken token)
    {
        var opinions = await _databaseContext.Opinions
            .Where(v => v.Author.Id == authorId)
            .ToListAsync(token);
        return opinions;
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _databaseContext.SaveChangesAsync(token);
    }
}
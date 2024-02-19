using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookLibrary.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public CommentRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Create(Comment comment, CancellationToken token)
    {
        await _databaseContext.Comments.AddAsync(comment, token);
    }

    public async Task<Result> Remove(Guid id, CancellationToken token)
    {
        var commentResult = await FindById(id, token);
        if (commentResult.IsFailed)
            return Result.Fail(commentResult.Errors.Last().Message);
        
        _databaseContext.Comments.Remove(commentResult.Value);
        return Result.Ok();
    }

    public async Task<Result<Comment>> FindById(Guid id, CancellationToken token)
    {
        var comment = await _databaseContext.Comments.FirstOrDefaultAsync(v => v.Id == id, token);
        if (comment is null)
            return Result.Fail("Not found comment");

        return Result.Ok(comment);
    }

    public async Task<List<Comment>> FindByAuthorId(Guid authorId, CancellationToken token)
    {
        var comments = await _databaseContext.Comments
            .Where(v => v.Author.Id == authorId)
            .ToListAsync(token);

        return comments;
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _databaseContext.SaveChangesAsync(token);
    }
}
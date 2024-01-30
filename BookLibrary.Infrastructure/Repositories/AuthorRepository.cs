using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly DatabaseContext _context;
    
    public AuthorRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task Create(Author author, CancellationToken token)
    {
        await _context.Authors.AddAsync(author, token);
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        var author = await FindUnique(id, token);
        if (author.IsFailed) return;
        
        _context.Authors.Remove(author.Value);
        await _context.SaveChangesAsync(token);
    }

    public async Task<Result<Author>> FindUnique(Guid id, CancellationToken token)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id, token);
        if (author is null)
            return Result.Fail("Not found author");
        
        return Result.Ok(author);
    }

    public async Task<List<Author>> FindAll(CancellationToken token)
    {
        var authors = await _context.Authors.ToListAsync(token);
        return authors;
    }
}
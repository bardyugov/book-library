using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DatabaseContext _context;
    
    public BookRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task Create(Book book, CancellationToken token)
    {
        await _context.Books.AddAsync(book, token);
    }

    public async Task<Result<Book>> FindByName(string name, CancellationToken token)
    {
        var isFindBook = await _context.Books.FirstOrDefaultAsync(b => b.Name == name, token);
        if (isFindBook is null)
            return Result.Fail("Book not found");

        return isFindBook;
    }

    public async Task<Result<Book>> FindById(Guid id, CancellationToken token)
    {
        var isFindBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id, token);
        if (isFindBook is null)
            return Result.Fail("Book not found");

        return isFindBook;
    }

    public async Task<Result<Book>> Remove(Guid id, CancellationToken token)
    {
        var result = await FindById(id, token);
        if (result.IsFailed)
            return result;

        return result.Value;
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }
}
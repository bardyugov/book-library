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
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == name, token);
        if (book is null)
            return Result.Fail("Book not found");

        return book;
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

    public async Task<Result<Book>> FindByNameWithRelations(string name, CancellationToken token)
    {
        var book = await _context.Books
            .Include(v => v.Complaints)
            .Include(v => v.Opinions)
            .Include(v => v.Comments)
            .Include(v => v.Author)
            .FirstOrDefaultAsync(v => v.Name == name, token);

        if (book is null)
            return Result.Fail("Not found book");

        return Result.Ok(book);
    }

    public Task<List<Book>> FindManyWithAuthorId(Guid id, CancellationToken token)
    {
        return _context.Books
            .Include(v => v.Comments)
            .Include(v => v.Complaints)
            .Include(v => v.Opinions)
            .Where(v => v.Author.Id == id)
            .ToListAsync(token);
    }

    public async Task<Result<Book>> UpdateCountReaders(Guid id, int count, CancellationToken token)
    {
        var isFindBook = await FindById(id, token);
        if (isFindBook.IsFailed)
            return isFindBook;

        isFindBook.Value.CountReaders = count;
        await _context.SaveChangesAsync(token);
        
        return isFindBook;
    }
}
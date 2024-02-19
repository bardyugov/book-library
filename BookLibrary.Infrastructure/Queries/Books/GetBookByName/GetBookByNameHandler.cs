using BookLibrary.Application.Queries.Books.GetBookByNameQuery;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Books.GetBookByName;

public class GetBookByNameHandler : IRequestHandler<GetBookByNameQuery, Result<GetBookByNameQueryResult>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public GetBookByNameHandler(IAuthenticationService authenticationService, IBookRepository bookRepository)
    {
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }

    private GetBookByNameQueryResult MapToResult(Book book)
    {
        List<Comment> comments = book.Comments
            .Select(v => new Comment()
            {
                CreateDate = v.CreateDate,
                Content = v.Content,
                Id = v.Id,
                UpdateDate = v.UpdateDate
            })
            .ToList();

        List<Opinion> opinions = book.Opinions
            .Select(v => new Opinion()
            {
                Id = v.Id,
                CreateDate = v.CreateDate,
                UpdateDate = v.UpdateDate,
                Reaction = v.Reaction
            })
            .ToList();

        List<Complaint> complaints = book.Complaints
            .Select(v => new Complaint()
            {
                CreateDate = v.CreateDate,
                Content = v.Content,
                Id = v.Id,
                UpdateDate = v.UpdateDate
            })
            .ToList();
        
        return new GetBookByNameQueryResult()
        {
            Id = book.Id,
            Name = book.Name,
            Description = book.Description,
            Path = book.Path,
            CountReaders = book.CountReaders,
            AuthorName = book.Author.Name,
            Comments = comments,
            Complaints = complaints,
            Opinions = opinions,
            CreateDate = book.CreateDate,
            UpdateDate = book.UpdateDate
        };
    }
    
    public async Task<Result<GetBookByNameQueryResult>> Handle(GetBookByNameQuery request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var isFindBook = await _bookRepository.FindByNameWithRelations(request.Name, cancellationToken);
        if (isFindBook.IsFailed)
            return Result.Fail(isFindBook.Errors.Last().Message);
        
        if (author.Id != isFindBook.Value.Author.Id)
        {
            isFindBook.Value.CountReaders += 1;
            await _bookRepository.UpdateCountReaders(isFindBook.Value.Id, isFindBook.Value.CountReaders,
                cancellationToken);
        }
        
        var result = MapToResult(isFindBook.Value);
        return Result.Ok(result);
    }
}
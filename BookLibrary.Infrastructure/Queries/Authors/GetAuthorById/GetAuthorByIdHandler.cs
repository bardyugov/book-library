using BookLibrary.Application.Queries.Authors;
using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Authors.GetAuthorById;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, Result<Author>>
{
    private readonly IAuthorRepository _authorRepository;
    
    public GetAuthorByIdHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Result<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var isAuthor = await _authorRepository.FindUnique(request.Id, cancellationToken);
        if (isAuthor.IsFailed)
        {
            return Result.Fail(isAuthor.Errors.Last().Message);
        }

        return Result.Ok(isAuthor.Value);
    }
}
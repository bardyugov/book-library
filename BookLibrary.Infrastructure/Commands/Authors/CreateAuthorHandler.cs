using BookLibrary.Application.Commands.Authors;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Authors;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<Author>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IJwtService _jwtService;
    
    public CreateAuthorHandler(IAuthorRepository authorRepository, IJwtService jwtService)
    {
        _authorRepository = authorRepository;
        _jwtService = jwtService;
    }

    public async Task<Result<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var isFindAuthor = await _authorRepository.FindByEmail(request.Email, cancellationToken);
        if (isFindAuthor.IsFailed)
        {
            return Result.Fail(isFindAuthor.Errors);
        }
        return new Author();
    }
}
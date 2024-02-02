using BookLibrary.Application.Commands.Authors.Create;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Authors.Create;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<CreateAuthorCommandResult>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public CreateAuthorHandler(
        IAuthorRepository authorRepository,
        IPasswordService passwordService,
        IJwtService jwtService)
    {
        _authorRepository = authorRepository;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<Result<CreateAuthorCommandResult>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var isFindAuthor = await _authorRepository.FindByEmail(request.Email, cancellationToken);
        if (isFindAuthor.IsSuccess)
            return Result.Fail("Author with this email already exist");
        
        var hashPassword = _passwordService.Hash(request.Password);

        var author = new Author()
        {
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            Email = request.Email,
            Password = hashPassword,
            Age = request.Age
        };
        
        await _authorRepository.Create(author, cancellationToken);
        var token = _jwtService.GenerateToken(author);
        await _authorRepository.SaveChangesAsync(cancellationToken);
        
        var result = new CreateAuthorCommandResult(token, author);
        
        return result;
    }
}
using BookLibrary.Application.Commands.Authors.Create;
using BookLibrary.Application.Commands.Authors.Login;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Authors.Login;

public class LoginAuthorHandler : IRequestHandler<LoginAuthorCommand, Result<CreateAuthorCommandResult>>
{
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IAuthorRepository _authorRepository;
    
    public LoginAuthorHandler(
        IJwtService jwtService,
        IPasswordService passwordService,
        IAuthorRepository authorRepository
        )
    {
        _jwtService = jwtService;
        _passwordService = passwordService;
        _authorRepository = authorRepository;
    }
    
    public async Task<Result<CreateAuthorCommandResult>> Handle(LoginAuthorCommand request, CancellationToken cancellationToken)
    {
        var isFindAuthor = await _authorRepository.FindByEmail(request.Email, cancellationToken);
        if (isFindAuthor.IsFailed)
            return Result.Fail(isFindAuthor.Errors.Last().Message);

        var author = isFindAuthor.Value;
        var isVerifyPassword = _passwordService.Verify(request.Password, author.Password);
        if (!isVerifyPassword)
            return Result.Fail("Not valid password");

        var token = _jwtService.GenerateToken(author);
        var result = new CreateAuthorCommandResult(token, author);

        return Result.Ok(result);
    }
}
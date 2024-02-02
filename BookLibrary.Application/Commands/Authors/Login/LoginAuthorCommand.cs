using BookLibrary.Application.Commands.Authors.Create;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Authors.Login;

public class LoginAuthorCommand : IRequest<Result<CreateAuthorCommandResult>>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}
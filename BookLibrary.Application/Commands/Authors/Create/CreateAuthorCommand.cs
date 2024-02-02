using MediatR;
using BookLibrary.Domain.Models;
using FluentResults;

namespace BookLibrary.Application.Commands.Authors.Create;

public class CreateAuthorCommand : IRequest<Result<CreateAuthorCommandResult>>
{
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Patronymic { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public int Age { get; set; }
    
}
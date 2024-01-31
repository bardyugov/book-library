using MediatR;
using BookLibrary.Domain.Models;

namespace BookLibrary.Application.Commands.Authors;

public class CreateAuthorCommand : IRequest<Author>
{
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Patronymic { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public int Age { get; set; }
    
}
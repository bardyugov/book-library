using BookLibrary.Application.Commands.Authors;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Authors;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Author>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IJwtService _jwtService;
    
    public CreateAuthorHandler(IAuthorRepository authorRepository, IJwtService jwtService)
    {
        _authorRepository = authorRepository;
        _jwtService = jwtService;
    }

    public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var newAuthor = new Author
        {
            Name = "dasda",
            Surname = "dasdada",
            Password = "dasdas",
            Age = 25,
            Patronymic = "Dasda",
        };
        var token =  _jwtService.GenerateToken(newAuthor);
        Console.WriteLine(token);
        return newAuthor;
    }
}
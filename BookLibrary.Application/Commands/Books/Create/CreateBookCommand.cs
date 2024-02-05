using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Books.Create;

public class CreateBookCommand : IRequest<Result<Book>>
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public byte[] Path { get; set; }
    
    public Author Author { get; set; }
}
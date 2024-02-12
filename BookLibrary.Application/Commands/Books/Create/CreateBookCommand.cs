using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Application.Commands.Books.Create;

public class CreateBookCommand : IRequest<Result<Book>>
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public IFormFile File { get; set; }
}
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Queries.Books;

public class GetBookByNameQuery : IRequest<Result<Book>>
{
    public string Name { get; set; }
}
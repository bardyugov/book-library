using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Queries.Books.GetBookByNameQuery;

public class GetBookByNameQuery : IRequest<Result<GetBookByNameQueryResult>>
{
    public string Name { get; set; }

    public GetBookByNameQuery(string name)
    {
        Name = name;
    }   
}
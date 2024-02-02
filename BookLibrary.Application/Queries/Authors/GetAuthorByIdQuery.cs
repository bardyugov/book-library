using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Queries.Authors;

public class GetAuthorByIdQuery : IRequest<Result<Author>>
{
    public Guid Id { get; set; }

    public GetAuthorByIdQuery(Guid id)
    {
        Id = id;
    }
}
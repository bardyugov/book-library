using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Queries.Opinions.GetOpinionsByBookId;

public class GetOpinionsByBookIdQuery : IRequest<Result<List<Opinion>>>
{
    public Guid Id { get; set; }

    public GetOpinionsByBookIdQuery(Guid id)
    {
        Id = id;
    }
}
using BookLibrary.Application.Queries.Opinions.GetOpinionsByBookId;
using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Opinions.GetOpinionsByBookId;

public class GetOpinionsByBookIdHandler : IRequestHandler<GetOpinionsByBookIdQuery, Result<List<Opinion>>>
{
    private readonly IOpinionRepository _opinionRepository;
    
    
    public GetOpinionsByBookIdHandler(IOpinionRepository opinionRepository)
    {
        _opinionRepository = opinionRepository;
    }

    public async Task<Result<List<Opinion>>> Handle(GetOpinionsByBookIdQuery request, CancellationToken cancellationToken)
    {
        var opinions = await _opinionRepository.FindByBookId(request.Id, cancellationToken);
        return Result.Ok(opinions);
    }
}
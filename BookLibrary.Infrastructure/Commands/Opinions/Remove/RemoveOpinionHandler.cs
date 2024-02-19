using BookLibrary.Application.Commands.Opinions.Remove;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Opinions.Remove;

public class RemoveOpinionHandler : IRequestHandler<RemoveOpinionCommand, Result<string>>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly IAuthenticationService _authenticationService;

    public RemoveOpinionHandler(IOpinionRepository opinionRepository, IAuthenticationService authenticationService)
    {
        _opinionRepository = opinionRepository;
        _authenticationService = authenticationService;
    }
    
    public async Task<Result<string>> Handle(RemoveOpinionCommand request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var opinions = await _opinionRepository.FindByAuthorId(author.Id, cancellationToken);
        var isFind = opinions.Exists(v => v.Id == request.Id);
        if (!isFind)
            return Result.Fail("Not found opinions");

        await _opinionRepository.Remove(request.Id, cancellationToken);
        await _opinionRepository.SaveChangesAsync(cancellationToken);
        
        return Result.Ok("Success remove");

    }
}
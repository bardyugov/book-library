using BookLibrary.Application.Commands.Opinions.Create;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Opinions.Create;

public class CreateOpinionHandler : IRequestHandler<CreateOpinionCommand, Result<Opinion>>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public CreateOpinionHandler(
        IOpinionRepository opinionRepository, 
        IAuthenticationService authenticationService,
        IBookRepository bookRepository
        )
    {
        _opinionRepository = opinionRepository;
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<Opinion>> Handle(CreateOpinionCommand request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var isFindBook = await _bookRepository.FindById(request.BookId, cancellationToken);
        if (isFindBook.IsFailed)
            return Result.Fail(isFindBook.Errors.Last().Message);

        var newOpinion = new Opinion()
        {
            Author = author,
            Book = isFindBook.Value,
            Reaction = request.Reaction
        };

        await _opinionRepository.Create(newOpinion, cancellationToken);
        await _opinionRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok(newOpinion);
    }
}
using BookLibrary.Application.Commands.Comments.Remove;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Comments.Remove;

public class RemoveCommentHandler : IRequestHandler<RemoveCommentCommand, Result<string>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthenticationService _authenticationService;

    public RemoveCommentHandler(ICommentRepository commentRepository, IAuthenticationService authenticationService)
    {
        _commentRepository = commentRepository;
        _authenticationService = authenticationService;
    }
    
    public async Task<Result<string>> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var comments = await _commentRepository.FindByAuthorId(author.Id, cancellationToken);
        var isFindComment = comments.Exists(v => v.Id == request.CommentId);
        if (!isFindComment)
            return Result.Fail("Not found comment");
        
        await _commentRepository.Remove(request.CommentId, cancellationToken);
        await _commentRepository.SaveChangesAsync(cancellationToken);
        return Result.Ok("Success remove comment");
    }
}
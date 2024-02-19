using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Comments.Remove;

public class RemoveCommentCommand : IRequest<Result<string>>
{
    public Guid CommentId { get; set; }

    public RemoveCommentCommand(Guid commentId)
    {
        CommentId = commentId;
    }
}
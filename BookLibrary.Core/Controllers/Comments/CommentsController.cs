using BookLibrary.Application.Commands.Comments.Create;
using BookLibrary.Application.Commands.Comments.Remove;
using BookLibrary.Infrastructure.Services.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Comments;

[ApiController]
[Route("Comments/[action]")]
public class CommentsController : BaseController
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Create(CreateCommentCommand command, CancellationToken token)
    {
        var result = await _mediator.Send(command, token);
        return ConvertToActionResult(result);
    }

    [HttpDelete]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Remove([FromQuery(Name = "CommentId")] Guid id, CancellationToken token)
    {
        var result = await _mediator.Send(new RemoveCommentCommand(id), token);
        return ConvertToActionResult(result);
    }    
}
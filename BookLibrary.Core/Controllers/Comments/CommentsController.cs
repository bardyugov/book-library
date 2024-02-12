using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Comments;

[ApiController]
[Route("Comments/[action]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        throw new NotImplementedException();
    }
}
using BookLibrary.Application.Commands.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Authors;

[ApiController]
[Route("Authors/[action]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorCommand author)
    {
        var newAuth = await _mediator.Send(author);
        return Ok(newAuth.Value);
    }
}
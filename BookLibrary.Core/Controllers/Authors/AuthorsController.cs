using BookLibrary.Application.Commands.Authors;
using BookLibrary.Infrastructure.Services.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Authors;

[ApiController]
[Route("authors")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorCommand command)
    {
        var author = await _mediator.Send(command);
        return Ok(author);
    }

    [HttpGet]
    [Authorize(Policy = RolesConstants.Admin)]
    public async Task<object> Get()
    {
        return "Private route";
    }
    
    [HttpPut]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<object> Put()
    {
        return "Private route";
    }
}
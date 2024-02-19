using BookLibrary.Application.Commands.Authors.Create;
using BookLibrary.Application.Commands.Authors.Login;
using BookLibrary.Application.Queries.Authors;
using BookLibrary.Infrastructure.Services.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Authors;

[ApiController]
[Route("Authors/[action]")]
public class AuthorsController : BaseController
{
    private readonly IMediator _mediator;
    
    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateAuthorCommand author, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(author, cancellationToken);
        return ConvertToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginAuthorCommand author, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(author, cancellationToken);
        return ConvertToActionResult(result);
    }

    [HttpGet]
    [Authorize(Policy = RolesConstants.Admin)]
    public async Task<IActionResult> Get([FromQuery(Name = "Id")] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);
        return ConvertToActionResult(result);
    }
    
}
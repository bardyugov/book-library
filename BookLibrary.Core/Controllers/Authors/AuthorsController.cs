using BookLibrary.Application.Commands.Authors.Create;
using BookLibrary.Application.Commands.Authors.Login;
using BookLibrary.Application.Queries.Authors;
using BookLibrary.Application.Services;
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
    private readonly IAuthenticationService _authenticationService;
    
    public AuthorsController(IMediator mediator, IAuthenticationService authenticationService)
    {
        _mediator = mediator;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorCommand author, CancellationToken cancellationToken)
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
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Get([FromQuery(Name = "Id")] Guid id, CancellationToken cancellationToken)
    {
        await _authenticationService.Authenticate(HttpContext, cancellationToken);
        var result = await _mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);
        return ConvertToActionResult(result);
    }
    
}
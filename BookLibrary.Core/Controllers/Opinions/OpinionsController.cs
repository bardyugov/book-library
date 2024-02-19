using BookLibrary.Application.Commands.Opinions.Create;
using BookLibrary.Infrastructure.Services.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Opinions;

[ApiController]
[Route("Opinions/[action]")]
public class OpinionsController : BaseController
{
    private readonly IMediator _mediator;

    public OpinionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Create(CreateOpinionCommand command, CancellationToken token)
    {
        var result = await _mediator.Send(command, token);
        return ConvertToActionResult(result);
    }
    
    
}
using BookLibrary.Application.Commands.Opinions.Create;
using BookLibrary.Application.Commands.Opinions.Remove;
using BookLibrary.Application.Queries.Opinions.GetOpinionsByBookId;
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

    [HttpDelete]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Remove([FromQuery(Name = "Id")] Guid id, CancellationToken token)
    {
        var result = await _mediator.Send(new RemoveOpinionCommand(id), token);
        return ConvertToActionResult(result);
    }

    [HttpGet]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Get([FromQuery(Name = "BookId")] Guid bookId, CancellationToken token)
    {
        var result = await _mediator.Send(new GetOpinionsByBookIdQuery(bookId), token);
        return ConvertToActionResult(result);
    }
}
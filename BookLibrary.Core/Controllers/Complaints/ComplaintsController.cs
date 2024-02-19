using BookLibrary.Application.Commands.Complaints.Create;
using BookLibrary.Application.Queries.Complaints.GetComplaintsByBookId;
using BookLibrary.Infrastructure.Services.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Complaints;

[ApiController]
[Route("Complaints/[action]")]
public class ComplaintsController : BaseController
{
    private readonly IMediator _mediator;

    public ComplaintsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Create(CreateComplaintCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return ConvertToActionResult(result);
    }

    [HttpGet]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Get([FromQuery(Name = "BookId")] Guid bookId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetComplaintsByBookIdQuery(bookId), cancellationToken);
        return ConvertToActionResult(result);
    }
}
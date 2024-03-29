using BookLibrary.Application.Commands.Books.Create;
using BookLibrary.Domain.Models;
using BookLibrary.Application.Queries.Books.GetBookByNameQuery;
using BookLibrary.Application.Queries.Books.GetBooksByAuthorIdQuery;
using BookLibrary.Infrastructure.Services.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Core.Controllers.Books;

[ApiController]
[Route("Books/[action]")]
public class BooksController : BaseController
{
    private readonly IMediator _mediator;
    
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Create(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return ConvertToActionResult(result);
    }

    [HttpGet]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Find([FromQuery(Name = "Name")] string name, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookByNameQuery(name), cancellationToken);
        return ConvertToActionResult(result);
    }

    [HttpGet]
    [Authorize(Policy = RolesConstants.User)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBooksByAuthorIdQuery(), cancellationToken);
        return ConvertToActionResult<List<Book>>(result);
    }
}   
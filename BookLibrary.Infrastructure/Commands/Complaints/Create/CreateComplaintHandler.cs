using BookLibrary.Application.Commands.Complaints.Create;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Complaints.Create;

public class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, Result<Complaint>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public CreateComplaintHandler(
        IComplaintRepository complaintRepository, 
        IAuthenticationService authenticationService,
        IBookRepository bookRepository
        )
    {
        _complaintRepository = complaintRepository;
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }

    public async Task<Result<Complaint>> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var bookResult = await _bookRepository.FindById(request.BookId, cancellationToken);
        if (bookResult.IsFailed)
            return Result.Fail(bookResult.Errors.Last().Message);

        var newComplaint = new Complaint()
        {
            Author = author,
            Book = bookResult.Value,
            Content = request.Content
        };

        await _complaintRepository.Create(newComplaint, cancellationToken);
        await _complaintRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok(newComplaint);
    }
}
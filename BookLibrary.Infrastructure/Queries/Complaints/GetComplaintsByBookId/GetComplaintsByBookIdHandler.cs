using BookLibrary.Application.Queries.Complaints.GetComplaintsByBookId;
using BookLibrary.Application.Repositories;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Queries.Complaints.GetComplaintsByBookId;

public class GetComplaintsByBookIdHandler : IRequestHandler<GetComplaintsByBookIdQuery, Result<List<Complaint>>>
{
    private readonly IComplaintRepository _complaintRepository;

    public GetComplaintsByBookIdHandler(IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }
    
    public async Task<Result<List<Complaint>>> Handle(GetComplaintsByBookIdQuery request, CancellationToken cancellationToken)
    {
        var complaints = await _complaintRepository.FindByBookId(request.BookId, cancellationToken);
        return Result.Ok(complaints);
    }
}
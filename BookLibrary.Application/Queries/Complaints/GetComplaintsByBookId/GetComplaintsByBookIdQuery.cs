using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Queries.Complaints.GetComplaintsByBookId;

public class GetComplaintsByBookIdQuery : IRequest<Result<List<Complaint>>>
{
    public Guid BookId { get; set; }

    public GetComplaintsByBookIdQuery(Guid id)
    {
        BookId = id;
    }
}
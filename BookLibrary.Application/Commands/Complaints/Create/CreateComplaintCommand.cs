using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Complaints.Create;

public class CreateComplaintCommand : IRequest<Result<Complaint>>
{
    public string Content { get; set; }
    
    public Guid BookId { get; set; }
}
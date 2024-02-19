using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Comments.Create;

public class CreateCommentCommand : IRequest<Result<Comment>>
{
    public string Content { get; set; }
    
    public string NameBook { get; set; }
}
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Opinions.Create;

public class CreateOpinionCommand : IRequest<Result<Opinion>>
{
    public Reaction Reaction { get; set; }
    
    public Guid BookId { get; set; }
}
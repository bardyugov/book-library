using FluentResults;
using MediatR;

namespace BookLibrary.Application.Commands.Opinions.Remove;

public class RemoveOpinionCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }

    public RemoveOpinionCommand(Guid id)
    {
        Id = id;
    }
}
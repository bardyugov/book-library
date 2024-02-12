using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Application.Services;

public interface IFileService
{
    Task<Result> Save(IFormFile file, Guid id, CancellationToken token);
}
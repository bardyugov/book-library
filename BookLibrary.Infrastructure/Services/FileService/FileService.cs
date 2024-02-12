using BookLibrary.Application.Services;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BookLibrary.Infrastructure.Services.FileService;

public class FileService : IFileService
{
    private const string FolderName = "wwwroot";

    private bool GetIsFormat(IFormFile file)
    {
        return file.FileName.Contains(".pdf") || file.FileName.Contains(".docx");
    }
    public async Task<Result> Save(IFormFile file, Guid id, CancellationToken token)
    {
        var isFormat = GetIsFormat(file);
        if (!isFormat)
            return Result.Fail("Not valid format book");
        
        var currentDirectory = Directory.GetCurrentDirectory();
        var folderPath = currentDirectory + "/" + FolderName;

        var format = file.FileName.Split(".").Last();
        var fileName = id + "." + format;

        await using (FileStream stream = new FileStream(folderPath + "/" + fileName, FileMode.Create))
        {
            await file.CopyToAsync(stream, token);
        }
        
        return Result.Ok();
    }
}
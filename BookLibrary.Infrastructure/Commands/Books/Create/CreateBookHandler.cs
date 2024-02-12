using BookLibrary.Application.Commands.Books.Create;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Books.Create;

public class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<Book>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IFileService _fileService;
    private readonly IAuthenticationService _authenticationService;

    public CreateBookHandler(IBookRepository repository, IFileService fileService, IAuthenticationService authenticationService)
    {
        _bookRepository = repository;
        _fileService = fileService;
        _authenticationService = authenticationService;
    }
    
    public async Task<Result<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var isFindBook = await _bookRepository.FindByName(request.Name, cancellationToken);
        if (isFindBook.IsSuccess)
            return Result.Fail($"Book with Name {isFindBook.Value.Name} already exist");

        var newBook = new Book()
        {
            Name = request.Name,
            Description = request.Description,
            CountReaders = 0,
            Author = author
        };

        var format = "." + request.File.FileName.Split(".").Last();
        newBook.Path = newBook.Id + format;
        await _bookRepository.Create(newBook, cancellationToken);
        
        var saveResult = await _fileService.Save(request.File, newBook.Id, cancellationToken);
        if (saveResult.IsFailed)
            return saveResult;
        
        await _bookRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok(newBook);
    }
}
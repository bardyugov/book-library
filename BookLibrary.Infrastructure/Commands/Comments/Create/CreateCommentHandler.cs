using BookLibrary.Application.Commands.Comments.Create;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using FluentResults;
using MediatR;

namespace BookLibrary.Infrastructure.Commands.Comments.Create;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Result<Comment>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IBookRepository _bookRepository;

    public CreateCommentHandler(
        ICommentRepository commentRepository, 
        IAuthenticationService authenticationService,
        IBookRepository bookRepository
        )
    {
        _commentRepository = commentRepository;
        _authenticationService = authenticationService;
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var author = await _authenticationService.Authenticate(cancellationToken);
        var book = await _bookRepository.FindByName(request.NameBook, cancellationToken);
        if (book.IsFailed)
            return Result.Fail(book.Errors.Last().Message);
        
        var newComment = new Comment()
        {
            Author = author,
            Content = request.Content,
            Book = book.Value
        };

        await _commentRepository.Create(newComment, cancellationToken);
        await _commentRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok(newComment);
    }
}
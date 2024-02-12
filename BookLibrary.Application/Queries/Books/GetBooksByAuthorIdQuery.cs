using BookLibrary.Domain.Models;
using MediatR;

namespace BookLibrary.Application.Queries.Books;

public class GetBooksByAuthorIdQuery : IRequest<List<Book>>
{
}
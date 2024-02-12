using BookLibrary.Domain.Models;
using MediatR;

namespace BookLibrary.Application.Queries.Books.GetBooksByAuthorIdQuery;

public class GetBooksByAuthorIdQuery : IRequest<List<Book>>
{
}
using BookLibrary.Domain.Models;

namespace BookLibrary.Application.Commands.Authors.Create;

public class CreateAuthorCommandResult
{
    public Author Author { get; set; }
    public string Token { get; set; }

    public CreateAuthorCommandResult(string token, Author author)
    {
        Author = author;
        Token = token;
    }
}
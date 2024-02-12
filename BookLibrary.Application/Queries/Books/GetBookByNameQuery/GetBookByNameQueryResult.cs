using BookLibrary.Domain.Models;

namespace BookLibrary.Application.Queries.Books.GetBookByNameQuery;

public class GetBookByNameQueryResult
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Path { get; set; }
    
    public int CountReaders { get; set; }
    
    public string AuthorName { get; set; } 
    
    public List<Comment> Comments { get; set; }
    
    public List<Opinion> Opinions { get; set; }
    
    public List<Complaint> Complaints { get; set; }
}
namespace BookLibrary.Domain.Models;

public class Opinion : BaseEntity
{
    public Reaction Reaction { get; set; }
    
    public Book Book { get; set; }
    
    public Author Author { get; set; }
}
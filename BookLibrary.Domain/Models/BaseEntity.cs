namespace BookLibrary.Domain.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
    public DateTime UpdateDate { get; set; }
}
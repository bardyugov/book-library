namespace BookLibrary.Domain.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdateDate { get; set; }
}
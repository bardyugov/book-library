using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain.Models;

public class Book : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string Path { get; set; }
    
    [Required]
    public string CountReaders { get; set; }
    
    [Required]
    public Author Author { get; set; } = null!;
    
    public List<Comment> Comments { get; set; }
    
    public List<Opinion> Opinions { get; set; }
    
    public List<Complaint> Complaints { get; set; }
}
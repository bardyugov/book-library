using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain.Models;

public class Complaint : BaseEntity
{
    [Required]
    public string Content { get; set; }
    
    public Author Author { get; set; }
    
    public Book Book { get; set; }
}
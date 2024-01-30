using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain.Models;

public class Author : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required]
    public string Patronymic { get; set; }
    
    [Required] 
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public int Age { get; set; }
    
    public List<Role> Roles { get; set; } = [ Role.User ];

    public List<Book> Books { get; set; }
    
    public List<Opinion> Opinions { get; set; }
    
    public List<Complaint> Complaints { get; set; }
}
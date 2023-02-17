using System.ComponentModel.DataAnnotations;

namespace Art_Gallery.Models;
public class Login
{
    [Required]
    [EmailAddress]
    public string Email {get; set;}
    [Required]
    public string Password {get; set;}
    [Required]
    public string role {get; set;}
}
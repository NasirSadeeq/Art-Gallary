using System.ComponentModel.DataAnnotations;
namespace Art_Gallery.Models;
public class Registration
{
    [Required]
    public string FirstName {get;set;}
    public string LastName {get;set;}
    [Required]
    public string MobileNo {get; set;}
    [Required]
    [Key]
    [EmailAddress]
    public string Email {get; set;}
    public string DateOfJoin {get; set;}
    [Required]
    public string Password {get; set;}
    [Required]
    public string Address {get; set;}
    [Required]
    public string role {get; set;}
}
using System.ComponentModel.DataAnnotations;

namespace Art_Gallery.Models;
public class GallaryRepresentative
{
    public GallaryRepresentative()
    {
        this.GrId=GenerateId();
    }
    public string GrId {get; set;}
    [Required]
     public string FirstName {get;set;}
    public string LastName {get;set;}
    public string MobileNo {get; set;}
    [Required]
    [Key]
    [EmailAddress]
    public string Email {get; set;}
    public string DateOfJoin {get; set;}
    [Required]
    public string Password {get; set;}
    public string Address {get; set;}

    private string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string res= new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            return "GR-"+res;
           // return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

}
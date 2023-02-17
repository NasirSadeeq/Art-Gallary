using System.ComponentModel.DataAnnotations;

namespace Art_Gallery.Models;
public class Gallary
{
    public Gallary()
    {
        this.GalleryId= GenerateId();
    }
    [Key]
    public string GalleryId {get;set;}
    [Required]
    public string GalleryName {get;set;}
    public int Quantity {get;set;}
    [Required]
    public string Location {get; set;}
    
    private string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string res= new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            return "G-"+res;
           // return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
}
using System.ComponentModel.DataAnnotations;

namespace Art_Gallery.Models;
public class Exhibation
{
    public Exhibation()
    {
        this.ExhibationId=GenerateId();
    }
    [Key]
    public string ExhibationId {get; set;}
    public string ArtId {get; set;}
    public string Name {get; set;}
    [Required]
    public string date {get; set;}
   // public int number {get; set;}
    [Required]
    public string Venue {get; set;}
    public string ContactNo{get;set;}


     private string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string res= new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            return "EXB-"+res;
           // return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
}
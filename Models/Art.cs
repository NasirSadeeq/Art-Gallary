using System.ComponentModel.DataAnnotations;

namespace Art_Gallery.Models;
public class Art
{
    public Art()
    {
        this.ArtId=GenerateId();

    }
    [Key]
    public string ArtId {get; set;}
    public string ArtistId{get;set;}
    public string Title {get; set;}
    public string ArtType {get; set;}
    public string Description {get; set;}
    public double ArtPrice {get; set;}
    public string imagePath{get;set;}

     private string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string res= new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            return "AR-"+res;
           // return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
}
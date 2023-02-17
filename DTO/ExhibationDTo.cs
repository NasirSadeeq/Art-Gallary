

using System.ComponentModel.DataAnnotations;

namespace Art_Gallery.DTO;
public class ExhibationDTO
{
     public string ExhibationId {get; set;}
     public string ArtisFname{get;set;}
     public string ArtistLname {get;set;}
     public string ArtistMobile {get;set;}
     public string Email {get;set;}
     public string ArtistAddress {get;set;}
    public string ArtId {get; set;}
    public string Name {get; set;}
    [Required]
    public string date {get; set;}
   // public int number {get; set;}
    [Required]
    public string Venue {get; set;}
    public string ContactNo{get;set;}
    public string Title {get; set;}
    public string ArtType {get; set;}
    public string Description {get; set;}
    public double ArtPrice {get; set;}
    public string imagePath{get;set;}
}
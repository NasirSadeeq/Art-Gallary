using System.ComponentModel.DataAnnotations;
namespace Art_Gallery.Models;
public class Cart
{

    [Required]
    [Key]
    public string BuyerId {get;set;}
    [Required]
    public  string ArtId {get;set;}
    [Required]
    public string ArtName {get;set;}
    [Required]
    public int Quantity { get; set;}
    [Required]
    public double UniteCast {get; set;}
    [Required]
    public double SubTotal {get; set;}


}
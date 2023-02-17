using System.ComponentModel.DataAnnotations;
namespace Art_Gallery.Models;
public class ShippingInfo
{
    [Required]
    [Key]
    public string ShippingId {get; set;}
    [Required]
    public int ShippingCost {get; set;}
    [Required]
    public string ShippingType {get;set;}
    [Required]
    public int ShippingRegionId {get; set;}
}
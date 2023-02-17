using System.ComponentModel.DataAnnotations;
namespace Art_Gallery.Models;
public class Order
{
    public Order()
    {
        this.OrderId=GenerateId();
    }
    public string OrderId {get;set;}
    [Required]
     public string UserId {get; set;}
     [Required]
     public string UserName {get;set;}
     [Required]
    public string Address {get; set;}
    public string MobileNo{get;set;}
    public string Email{get; set;}
    public DateTime date{get;set;}
    public string Status {get; set;}
    public List<Cart> items {get; set;}
    public double Grand_Total{get;set;}
   // [Required]
    //public string ShippingId {get; set;}

    private string GenerateId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string res= new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            return "OR-"+res;
           // return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
}
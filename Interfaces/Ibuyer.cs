
using Art_Gallery.DTO;
using Art_Gallery.Models;

namespace Art_Gallery.Interface;
public interface Ibuyer
{
    Task<Artist> GetArtist(string Email);
    Task<Cart> GetCartItem(string ArtId);
     Task<IEnumerable<Artist>> GetAllArtist();
     Task<Art> GetArtByArtist(string ArtistId);
     Task<Art> GetArtByType(string Type);
     Task<ExhibationDTO> GetExhibation(string ExhibationId);
     Task<IEnumerable<Exhibation>> GetAllExhibation();
    Task<Cart> AddToCart(Cart cart);
    Task RemoveFromCart(string ArtId);
    Task<Cart> UpdateCart(Cart cart);
    Task<IEnumerable<Cart>> GetAllCartItems(string BuyerId);
    Task<Order> PlaceOrder(string buyerid);
    Task<Order> Getorder(string OrderId);
    Task DeleteItem(string ArtId);
    Task<Buyers> UpdateProfile(Buyers ArtistDetails);


}